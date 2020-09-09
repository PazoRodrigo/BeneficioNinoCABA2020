
$(document).ready(function () {
    alertInfo('Aca');
    Inicio();
});

function Inicio() {
    LimpiarFormulario();
}

function LimpiarFormulario() {
    $("#TxtNroDocumento").val("");
    $("#TxtIdVoucher").val("");
    $("#TxtObservaciones").val("");
}
//Botones
$("body").on("click", "#BtnConfirmar", async function () {
    try {
        spinner();
        await ValidarConfirmarVoucher();
        let NroDocumento = $("#TxtNroDocumento").val("");
        let IdVoucher = $("#TxtIdVoucher").val("");
        let ObjVoucher = await Voucher.TraerUnoXDatos(NroDocumento, IdVoucher);
        await ObjVoucher.Confirmar();
        LimpiarFormulario();
        spinnerClose();
    } catch (error) {
        spinnerClose();
        alertInfo(error);
    }
});
$("body").on("click", "#BtnNuevoConfirmar", async function () {
    try {
        spinner();
        LimpiarFormulario();
        spinnerClose();
    } catch (error) {
        spinnerClose();
        alertInfo(error);
    }
});

async function ValidarConfirmarVoucher() {
    let sError = "";
    let NroDocumento = $("#TxtNroDocumento").val("");
    let IdVoucher = $("#TxtIdVoucher").val("");
    if (sError.length > 0) {
        throw '<p class="pl-4 text-left">' + sError + "</p>";
    }
}

