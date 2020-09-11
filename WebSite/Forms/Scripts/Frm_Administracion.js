
var _ObjTitular;
var _ListaFamiliares = [];
var _ListaBeneficios = [];
var _ListaVouchers = [];

function ContenidoOcultarTodo() {
    $("#TituloContenido").text('');
    $("#LblCantidadRegistros").text('');
    $("#divReimpresionVoucher").css('display', 'none');
    $("#divReporteInscripciones").css('display', 'none');
    $("#divReporteEntregados").css('display', 'none');
    $("#divReporteEtiquetasEntregas").css('display', 'none');

}
//Botones Menu
$("body").on("click", "#BtnMenuReimpresion", async function () {
    try {
        ContenidoOcultarTodo();
        LimpiarReimpresion();
        $("#TituloContenido").text('Reimpresión de Vouchers');
        $("#divReimpresionVoucher").css('display', 'block');
        $("#P2_NroDocumentoTitular").focus();
    } catch (error) {
        spinnerClose();
        alertInfo(error);
    }
});
$("body").on("click", "#BtnMenuReporteInscripciones", async function () {
    try {
        ContenidoOcultarTodo();
        $("#TituloContenido").text('Reporte de Inscripciones');
        let ListaImpresion = await Voucher.TraerTodos();
        $("#LblCantidadRegistros").text(ListaImpresion.length - 1);
        await Voucher.ArmarGrillaImpresion('GrillaReporteInscripciones', ListaImpresion);
        $("#divReporteInscripciones").css('display', 'block');
    } catch (error) {
        spinnerClose();
        alertInfo(error);
    }
});
$("body").on("click", "#BtnMenuReporteEntregados", async function () {
    try {
        ContenidoOcultarTodo();
        $("#TituloContenido").text('Reporte de Beneficios Entregados');
        $("#divReporteEntregados").css('display', 'block');
    } catch (error) {
        spinnerClose();
        alertInfo(error);
    }
});
$("body").on("click", "#BtnMenuReporteEtiquetasEntregas", async function () {
    try {
        ContenidoOcultarTodo();
        $("#TituloContenido").text('Etiquetas de Entrega');
        $("#divReporteEtiquetasEntregas").css('display', 'block');
    } catch (error) {
        spinnerClose();
        alertInfo(error);
    }
});

//Reimpresion
function LimpiarReimpresion() {
    _ListaBeneficios = [];
    _ListaFamiliares = [];
    _ListaVouchers = [];
    $("#P2_NroDocumentoTitular").val("");
    $("#P2_ApellidoNombreTitular").val("");
    $("#BtnAceptarTitular").css("display", "none");
    $(".DatosFamiliares").hide();
}
$("body").on("click", "#BtnBuscarTitular", async function () {
    try {
        spinner();
        _ObjTitular = undefined;
        _ListaFamiliares = [];
        _ListaBeneficios = [];
        let NroDocumento = $("#P2_NroDocumentoTitular").val();
        if (NroDocumento.length <= 6) {
            throw "Verifique el Nro. de Documento";
        }
        _ObjTitular = await Titular.TraerUnoXNroDocumento(NroDocumento);
        $("#P2_ApellidoNombreTitular").val(_ObjTitular.ApellidoNombre);
        $("#BtnAceptarTitular").css("display", "block");
        spinnerClose();
    } catch (error) {
        spinnerClose();
        alertInfo(error);
    }
});
$("body").on("click", "#BtnAceptarTitular", async function () {
    try {
        spinner();
        _ListaFamiliares = await Familiar.TraerTodosXTitular(_ObjTitular.IdEntidad);
        _ListaBeneficios = await Voucher.TraerTodosxAfiliado(_ObjTitular.IdEntidad);
        if (_ListaFamiliares?.length == 0) {
            throw "Usted no tiene posibles beneficiarios del Beneficio";
        }
        await Familiar.ArmarGrilla(
            "GrillaFamiliares",
            _ListaFamiliares,
            "EventoFamiliarSeleccionado"
        );
        if (_ListaBeneficios.length > 0) {
            await MarcarBeneficiarios();
        }
        $(".DatosFamiliares").show();
        spinnerClose();
    } catch (error) {
        spinnerClose();
        alertInfo(error);
    }
});
async function MarcarBeneficiarios() {
    if (_ListaFamiliares.length > 0) {
        let f = 0;
        while (f <= _ListaFamiliares.length - 1) {
            let Marcado = false;
            let b = 0;
            while (b <= _ListaBeneficios.length - 1) {
                if (_ListaBeneficios[b].IdFamiliar == _ListaFamiliares[f].IdEntidad) {
                    Marcado = true;
                }
                b++;
            }
            if (Marcado) {
                $("#Chk_" + _ListaFamiliares[f].IdEntidad + "").prop("checked", true);
            }
            f++;
        }
    }
}
$("body").on("click", "#BtnReimprimirVoucher", async function () {
    try {
        spinner();
        await Voucher.ArmarGrilla(
            "GrillaBeneficiariosReImpresion",
            _ListaBeneficios,
            _ListaFamiliares
        );
        await ImprimirVoucher();
        spinnerClose();
    } catch (error) {
        spinnerClose();
        alertInfo(
            error
        );
    }
});
async function ImprimirVoucher() {
    try {
        spinner();
        let divbol = $("#ReimpresionVoucher");
        let divContents = divbol.html();
        let _window = window.open("", "Impresion Voucher");
        if (_window != null) {
            _window.document.write(
                '<html><head><link rel="stylesheet" media="print,screen"  type="text/css" href="../Styles/bootstrap.css"/></head><body>'
            );
            _window.document.write(divContents);
            _window.document.write("</body></html>");
            _window.document.close();
            _window.focus();
            _window.document.body.onload = function () {
                _window.print();
                _window.close();
            };
        } else {
            throw new Error(
                "Desbloquee las venranas emergentes para imprimir por favor"
            );
        }
        spinnerClose();
    } catch (err) {
        spinnerClose();
        alertAlerta(err);
    }
}
