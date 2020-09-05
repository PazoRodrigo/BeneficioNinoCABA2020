var _TempObjDomicilio;
var _ObjTitular;
var _ListaFamiliares = [];
var _ListaBeneficiarios = [];

$(document).ready(function () {
    Inicio();
});

function Inicio() {
    LimpiarFormulario();
    _TempObjDomicilio = new Domicilio;
}

function LimpiarFormulario() {
    //Paso 1
    $("#P1_ChkAcepto").prop("checked", false);
    //Paso 2
    $("#P2_NroDocumentoTitular").val('17109689'); // Borrar
    $("#P2_ApellidoNombreTitular").val('');
    $("#BtnAceptarTitular").css('display', 'none');
    $(".DatosFamiliares").hide();
    //Paso 3
    $("#P3_Domicilio").val('');
    $("#P3_CodigoPostal").val('');
    $("#P3_Localidad").val('');
    $("#P3_Provincia").val('');
    $("#P3_Telefono").val('');
    $("#P3_CorreoElectronico").val('');
}
// Steps
$("#pasosBeneficiario").steps({
    headerTag: "h3",
    bodyTag: "section",
    transitionEffect: "slideLeft",
    enableFinishButton: false,
    enablePagination: true,
    enableAllSteps: false,
    autoFocus: true,
    labels: {
        finish: "Gracias",
        next: "Próximo",
        previous: "Anterior"
    }

});


//Botones
$("body").on("click", "#BtnBuscarTitular", async function () {
    try {
        spinner();
        _ObjTitular = undefined;
        _ListaFamiliares = [];
        _ListaBeneficiarios = [];
        let NroDocumento = $("#P2_NroDocumentoTitular").val();
        if (NroDocumento.length <= 6) {
            throw 'Verifique el Nro. de Documento';
        }
        _ObjTitular = await Titular.TraerUnoXNroDocumento(NroDocumento);
        $("#P2_ApellidoNombreTitular").val(_ObjTitular.ApellidoNombre);
        $("#BtnAceptarTitular").css('display', 'block');
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
        if (_ListaFamiliares?.length == 0) {
            throw 'Usted no tiene posibles beneficiarios del Beneficio';
        }
        await Familiar.ArmarGrilla('GrillaFamiliares', _ListaFamiliares)
        $(".DatosFamiliares").show();
        spinnerClose();
    } catch (error) {
        spinnerClose();
        alertInfo(error);
    }
});
$("body").on("click", "#BtnGenerarVoucher", async function () {
    try {
        await ValidarGenerarVoucher();
    } catch (error) {
        alertInfo('<b>Realice las correcciones informadas para Generar el Voucher</b><br><br>' + error);
    }
});

async function ValidarGenerarVoucher() {
    let sError = '';
    //Paso 1
    let Acepto = $("#P1_ChkAcepto").is(":checked");
    if (Acepto == false) {
        sError += '- Acepte las condiciones para continuar <br>';
    }
    //Paso 2
    if (_ListaBeneficiarios?.length == 0) {
        sError += '- Seleccione familiares para obtener el beneficio <br>';
    }
    //Paso 3
    MarcoDefault('P3_Domicilio');
    if ($("#P3_Domicilio").val().length == 0) {
        sError += '- Informe el Domicilio <br>';
        MarcoError('P3_Domicilio');
    }
    MarcoDefault('P3_CodigoPostal');
    if ($("#P3_CodigoPostal").val().length != 4) {
        sError += '- Informe el Código Postal <br>';
        MarcoError('P3_CodigoPostal');
    }
    MarcoDefault('P3_CodigoPostal');
    if ($("#P3_CodigoPostal").val().length == 0) {
        sError += '- Informe el Código Postal <br>';
        MarcoError('P3_CodigoPostal');
    } else {
        if ($("#P3_CodigoPostal").val().length != 4) {
            sError += '- El Código Postal debe componerse de 4 dígitos <br>';
            MarcoError('P3_CodigoPostal');
        }
    }
    MarcoDefault('P3_Localidad');
    if ($("#P3_Localidad").val().length != 4) {
        sError += '- Informe la Localidad <br>';
        MarcoError('P3_Localidad');
    }
    MarcoDefault('P3_Provincia');
    if ($("#P3_Provincia").val().length != 4) {
        sError += '- Informe la Provincia <br>';
        MarcoError('P3_Provincia');
    }
    MarcoDefault('P3_Telefono');
    if ($("#P3_Telefono").val().length == 0) {
        sError += '- Informe el Teléfono <br>';
        MarcoError('P3_Telefono');
    } else {
        if ($("#P3_Telefono").val().length != 10) {
            sError += '- El Teléfono debe componerse de 10 dígitos <br>';
            MarcoError('P3_Telefono');
        }
    }
    MarcoDefault('P3_CorreoElectronico');
    if ($("#P3_CorreoElectronico").val().length == 0) {
        sError += '- Informe el Correo Electrónico <br>';
        MarcoError('P3_CorreoElectronico');
    } else {
        let valido = await validarEmail($('#P3_CorreoElectronico').val());
        if (valido == false) {
            sError += '- El Correo Electrónico debe ser válido <br>';
            MarcoError('P3_CorreoElectronico');
        }
    }
    if (sError.length > 0) {
        throw '<p class="pl-4 text-left">' + sError + '</p>'
    }
}

//Eventos
$("body").on("keyup", "#P3_CodigoPostal", async function () {
    if (_TempObjDomicilio == null) {
        // Si no fue cargado anteriormente
        _TempObjDomicilio = new Domicilio();
    }
    let CodPos = $("#P3_CodigoPostal").val();
    $("#P3_Localidad").val("");
    $("#P3_Provincia").val("");
    $("#P3_CboLocalidad").css("display", "none");
    $("#P3_Localidad").css("display", "block");
    _TempObjDomicilio.CodigoPostal = 0;
    if (CodPos.length == 4) {
        let listaLocalidades = await Localidad.TraerTodasXCP(CodPos);
        if (listaLocalidades.length == 0) {
            $(this).css("background-color", "pink");
            _TempObjDomicilio.CodigoPostal = parseInt(0);
            _TempObjDomicilio.IdLocalidad = 0;
            $("#P3_Localidad").css("display", "block");
            $("#P3_CboLocalidad").css("display", "none");
        } else {
            $(this).css("background-color", "transparent");
            _TempObjDomicilio.CodigoPostal = parseInt(CodPos);
            if (listaLocalidades.length == parseInt(1)) {
                if (listaLocalidades.length == 1) {
                    await LlenarLocalidad(listaLocalidades[0], 2);
                    $("#P3_Localidad").css("display", "block");
                    $("#P3_CboLocalidad").css("display", "none");
                }
            } else {
                if (listaLocalidades.length > 1) {
                    _TempObjDomicilio.IdLocalidad = 0;
                    await Localidad.ArmarCombo(
                        listaLocalidades,
                        "P3_CboLocalidad",
                        "P3_IdCboLocalidad",
                        "P3_selectorLocalidad",
                        "P3_LodalidadSeleccionado",
                        "form-control"
                    );
                    $("#P3_Localidad").css("display", "none");
                    $("#P3_CboLocalidad").css("display", "block");
                }
            }
        }
    }
});
document.addEventListener("P3_LodalidadSeleccionado", async function (e) {
    try {
        let objSeleccionado = e.detail;
        await LlenarLocalidad(objSeleccionado);
    } catch (error) {
        alertAlerta(error);
    }
}, false);
async function LlenarLocalidad(objLocalidadBuscado) {
    let objProvinciaBuscada = await Provincia.TraerUna(
        objLocalidadBuscado.IdProvincia
    );
    $("#P3_Localidad").val(objLocalidadBuscado.Nombre);
    _TempObjDomicilio.IdLocalidad = objLocalidadBuscado.IdEntidad;
    $("#P3_Provincia").val(objProvinciaBuscada.Nombre);
}