﻿var _TempObjDomicilio;
var _ObjTitular;
var _ListaFamiliares = [];
var _ListaBeneficios = [];
var _ListaVouchers = [];
$(document).ready(function () {
    Inicio();
});

function Inicio() {
    LimpiarFormulario();
}

function LimpiarFormulario() {
    //Paso 1
    $("#P2_NroDocumentoTitular").val("");
    $("#P2_ApellidoNombreTitular").val("");
    $("#BtnAceptarTitular").css("display", "none");
    $(".DatosFamiliares").hide();
    //Paso 2
    $("#P3_Domicilio").val("");
    $("#P3_CodigoPostal").val("");
    $("#P3_Localidad").val("");
    $("#P3_CboLocalidad").css('display', 'none');
    $("#P3_Localidad").css('display', 'block');
    $("#P3_Provincia").val("");
    $("#P3_Telefono").val("");
    $("#P3_CorreoElectronico").val("");
    _TempObjDomicilio = new Domicilio();
    _ListaBeneficios = [];
    _ListaFamiliares = [];
    _ObjTitular = new Titular();
    // $("#P1_TextoAceptacion1").text('* Válido para participación de sorteos de premios en vivo.');
    // $("#P1_TextoAceptacion2").text('* Mediante el presente comprobante durante el mes de octubre se entregará y/o enviará a su domicilio registrado un juguete para el niño/a según grupo etario.');
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
        previous: "Anterior",
    },
});

//Botones
$("body").on("click", "#BtnBuscarTitular", async function () {
    try {

        spinner();
        _ObjTitular = undefined;
        _ListaFamiliares = [];
        _ListaBeneficios = [];
        $("#GrillaBeneficios").html('');
        let NroDocumento = $("#P2_NroDocumentoTitular").val();
        if (NroDocumento.length <= 6) {
            throw "Verifique el Nro. de Documento";
        }
        _ObjTitular = await Titular.TraerUnoXNroDocumento(NroDocumento);
        _ListaFamiliares = await Familiar.TraerTodosXTitular(_ObjTitular.IdEntidad);
        $("#P2_ApellidoNombreTitular").val(_ObjTitular.ApellidoNombre);
        $("#BtnAceptarTitular").css("display", "block");
        spinnerClose();
        //alertInfo('Ya casi estamos. En breve el beneficio de Entrega estará disponible para vos !');
    } catch (error) {
        spinnerClose();
        alertInfo(error);
    }
});
$("body").on("click", "#BtnAceptarTitular", async function () {
    try {

        spinner();
        _ListaBeneficios = await Beneficio.TraerTodosxAfiliado_BeneficioEntrega(_ObjTitular.IdEntidad);
        if (_ListaBeneficios?.length == 0) {
            _ObjTitular = null;
            LimpiarFormulario();
            throw "Usted no tiene Beneficios a ser Entregados";
        }
        console.log(_ListaBeneficios);
        let buscado = $.grep(_ListaBeneficios, function (entidad, index) {
            return entidad.FechaEntrega > 0;
        });
        console.log(buscado);
        if (buscado?.length > 0) {
            _ObjTitular = null;
            LimpiarFormulario();
            throw "Ya fue registrado su domicilio para la entrega.<br><br> Si necesita cambiar dicho domicilio contáctese a <br> <i> administrativa@utedyccapital.org.ar </i><br> <i>Teléfono : 11 6587 2073</i>";
        }
        await Beneficio.ArmarGrilla(
            "GrillaBeneficios",
            _ListaBeneficios,
            _ObjTitular
        );
        $(".DatosFamiliares").show();
        spinnerClose();

        //alertInfo('Ya casi estamos. En breve el beneficio de Entrega estará disponible para vos !');
    } catch (error) {
        spinnerClose();
        alertInfo(error);
    }
});
$("body").on("click", "#BtnGuardarDatos", async function () {
    try {

        spinner();
        await ValidarGuardarDatos();
        await EntregaBeneficio.GuardarEntregaBeneficio(_ObjTitular.IdEntidad, _TempObjDomicilio, $("#P3_CorreoElectronico").val().trim(), $("#P3_Telefono").val().trim());
        $("#DivMailEnviado").css('display', 'none');
        LimpiarFormulario();
        spinnerClose();
        alertOk('<b>Fue  realizada con éxito la carga de los datos de entrega.<b><br><br> Para consultas comuníquese con la Seccional Capital Federal.<br> <i>WhastApp: 54 9 11 6587-2073 / 54 9 11 2515-7856.</i>');
    } catch (error) {
        spinnerClose();
        alertInfo(
            "<br>Realice las correcciones informadas para Generar el Envío</br><br><br>" +
            error
        );
    }
});
// async function ImprimirVoucher() {
//     try {
//         spinner();
//         let divbol = $("#ImpresionVoucher");
//         let divContents = divbol.html();
//         let _window = window.open("", "Impresion Voucher");
//         if (_window != null) {
//             _window.document.write(
//                 '<html><head><link rel="stylesheet" media="print,screen"  type="text/css" href="../Styles/bootstrap.css"/></head><body>'
//             );
//             _window.document.write(divContents);
//             _window.document.write("</body></html>");
//             _window.document.close();
//             _window.focus();
//             _window.document.body.onload = function () {
//                 _window.print();
//                 _window.close();
//             };
//         } else {
//             throw new Error(
//                 "Desbloquee las ventanas emergentes para imprimir por favor"
//             );
//         }
//         spinnerClose();
//     } catch (err) {
//         spinnerClose();
//         alertAlerta(err);
//     }
// }
$("body").on("click", "#BtnConfirmacion", function () {
    alertInfo(
        "<b>Confirmación de Entrega</b><br><br><em>Utilice este link para Validar la entrega luego del Sorteo.</em>"
    );
});
$("body").on("click", "#BtnAyuda", function () {
    alertOk(
        "<b>Ayuda</b><br><br><em>Si necesitas asesoramiento para la carga del formulario comuníquese al 5277-6224 (LUN A VIER 10 A 18 HS)</em>"
    );
});
// $("body").on("click", "#BtnAgregarFamiliar", function () {
//     alertOk(
//         "<b>Agregar Familiar</b><br><br><em>Para la carga del familiar comuniquese al 5219-1592. (LUN A VIER 10 A 18 HS)<br>Deberá tener disponible el DNI del familiar y la partida de nacimiento.</em>"
//     );
// });
// $("body").on("click", "#BtnQuieroAfiliarme", function () {
//     alertOk(
//         "<b>Quiero Afiliarme</b><br><br><em>Por favor comuniquese al 5219-1591 <br> (LUN A VIER 10 A 18 HS)</em>"
//     );
// });

async function ValidarGuardarDatos() {
    let sError = "";
    //Paso 1
    //Paso 2
    //Paso 3
    MarcoDefault("P3_Domicilio");
    MarcoDefault("P3_CodigoPostal");
    MarcoDefault("P3_Localidad");
    MarcoDefault("P3_Provincia");

    if ($("#P3_Domicilio").val().length == 0) {
        sError += "- Informe el Domicilio <br>";
        MarcoError("P3_Domicilio");
    }
    _TempObjDomicilio.Domicilio = $("#P3_Domicilio").val();
    if ($("#P3_CodigoPostal").val().length == 0) {
        sError += "- Informe el Código Postal <br>";
        MarcoError("P3_CodigoPostal");
    } else {
        if ($("#P3_CodigoPostal").val().length != 4) {
            sError += "- El Código Postal debe componerse de 4 dígitos <br>";
            MarcoError("P3_CodigoPostal");
        } else {
            if (_TempObjDomicilio.CodigoPostal == 0) {
                sError += "- El Código Postal debe ser válido <br>";
                MarcoError("P3_CodigoPostal");
            }
        }
    }
    if (_TempObjDomicilio.IdLocalidad == 0) {
        sError += "- Informe la Localidad <br>";
        MarcoError("P3_Localidad");
    }
    if (_TempObjDomicilio.IdProvincia == 0) {
        sError += "- Informe la Provincia <br>";
        MarcoError("P3_Provincia");
    }
    MarcoDefault("P3_Telefono");
    if ($("#P3_Telefono").val().length == 0) {
        sError += "- Informe el Teléfono <br>";
        MarcoError("P3_Telefono");
    } else {
        if ($("#P3_Telefono").val().length != 10) {
            sError += "- El Teléfono debe componerse de 10 dígitos <br>";
            MarcoError("P3_Telefono");
        }
    }
    let TempCorreo = $("#P3_CorreoElectronico").val().trim();
    MarcoDefault("P3_CorreoElectronico");
    if (TempCorreo.length == 0) {
        sError += "- Informe el Correo Electrónico <br>";
        MarcoError("P3_CorreoElectronico");
    } else {
        let valido = await validarEmail(TempCorreo);
        if (valido == false) {
            sError += "- El Correo Electrónico debe ser válido <br>";
            MarcoError("P3_CorreoElectronico");
        }
    }
    if (sError.length > 0) {
        throw '<p class="pl-4 text-left">' + sError + "</p>";
    }
}

//Eventos
async function BuscadorLocalidad(MiElemento) {
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
        let listaLocalidades = await Localidad.TraerTodosXCodigoPostal(CodPos);
        if (listaLocalidades.length == 0) {
            $(this).css("background-color", "pink");
            _TempObjDomicilio.CodigoPostal = parseInt(0);
            _TempObjDomicilio.IdLocalidad = 0;
            $("#P3_Localidad").css("display", "block");
            $("#P3_CboLocalidad").css("display", "none");
        } else {
            $(this).css("background-color", "transparent");
            _TempObjDomicilio.CodigoPostal = parseInt(CodPos);
            if (parseInt(listaLocalidades.length) == 1) {
                if (listaLocalidades.length == 1) {
                    await LlenarLocalidad(listaLocalidades[0]);
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
}

//Escuchadores
document.addEventListener(
    "P3_LodalidadSeleccionado",
    async function (e) {
        try {
            let objSeleccionado = e.detail;
            await LlenarLocalidad(objSeleccionado);
        } catch (error) {
            alertAlerta(error);
        }
    },
    false
);
document.addEventListener(
    "EventoFamiliarSeleccionado",
    async function (e) {
        try {
            let objSeleccionado = e.detail;
            await AgregarBeneficiario(objSeleccionado);
        } catch (error) {
            alertAlerta(error);
        }
    },
    false
);

//Funciones
async function LlenarLocalidad(objLocalidadBuscado) {
    $("#P3_Localidad").val(objLocalidadBuscado.Nombre);
    let objProvinciaBuscada = await Provincia.TraerUna(
        objLocalidadBuscado.IdProvincia
    );
    _TempObjDomicilio.IdLocalidad = objLocalidadBuscado.IdEntidad;
    $("#P3_Provincia").val(objProvinciaBuscada.Nombre);
}
async function AgregarBeneficiario(ObjBeneficiario) {
    let ObjVoucher = new Voucher();
    ObjVoucher.IdTitular = ObjBeneficiario.NroAfiliado;
    ObjVoucher.IdFamiliar = ObjBeneficiario.IdEntidad;
    if (_ListaBeneficios?.length == 0) {
        _ListaBeneficios.push(ObjVoucher);
    } else {
        let buscado = $.grep(_ListaBeneficios, function (entidad, index) {
            return entidad.IdEntidad == ObjBeneficiario.IdEntidad;
        });
        if (buscado.length == 0) {
            _ListaBeneficios.push(ObjVoucher);
        } else {
            _ListaBeneficios = $.grep(_ListaBeneficios, function (entidad, index) {
                return entidad.IdEntidad != ObjBeneficiario.IdEntidad;
            });
        }
    }
}
