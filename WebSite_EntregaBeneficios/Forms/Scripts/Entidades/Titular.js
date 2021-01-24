class Titular {
    constructor() {
        this.IdEntidad = 0;
        this.NroAfiliado = 0;
        this.ApellidoNombre = '';
        this.NroDocumento = 0;
        this.IdSeccional = 0;

        this.CUIL = 0;
        this.CUITEmpresa = 0;
        this.RazonSocial = '';
        this.Beneficios = 0;

        this.ObjDomicilio;
    }

    static async TraerUnoXNroDocumento(NroDocumento) {
         let data = {
             "NroDocumento": NroDocumento
         };
         let entidad = 'Titulares';
         let metodo = 'TraerUnoXNroDocumento';
         let url = ApiURL + '/' + entidad + '/' + metodo;
         let datos = await TraerAPI(url, data);
         let result = [];
         $.each(datos, function (key, value) {
             result.push(llenarEntidadTitular(value));
         });
         return result[0];
    }
    static async TraerTodosConVoucher() {
        let entidad = 'Titulares';
        let metodo = 'TraerTodosConVoucher';
        let url = ApiURL + '/' + entidad + '/' + metodo;
        let datos = await TraerAPI(url);
        let result = [];
        $.each(datos, function (key, value) {
            result.push(llenarEntidadTitular(value));
        });
        return result;
    }
    // Herramientas
    static async ArmarGrillaImpresion(div, Lista) {
        $('#' + div + '').html('');
        let str = '';
        if (Lista.length > 0) {
            str += ' <table class="table table-bordered">';
            str += '     <thead>';
            str += '         <tr class="bg-primary text-light">';
            str += '             <th class="text-center" style="width: 30%;">Apellido / Nombre</th>';
            str += '             <th class="text-center" style="width: 15%;">Nro. Doc.</th>';
            str += '             <th class="text-center" style="width: 15%;">CUIT</th>';
            str += '             <th class="text-center" style="width: 35%;">Razón Social</th>';
            str += '             <th class="text-center">Benef.</th>';
            str += '         </tr>';
            str += '     </thead>';
            str += '     <tbody>';
            for (let item of Lista) {
                str += '    <tr>';
                str += '        <td class="text-left pl-1"> ' + item.ApellidoNombre + '</td>';
                str += '        <td class="text-center"> ' + Right('00000000' + item.NroDocumento, 8) + '</td>';
                str += '        <td class="text-right pr-4"><small> ' + item.CUITEmpresa + '</small></td>';
                str += '        <td class="text-right pr-4"><small> ' + Left(item.RazonSocial, 42) + '</small></td>';
                str += '        <td class="text-right pr-4"><small> ' + item.Beneficios + '</small></td>';
                str += '    </tr>';
            }
            str += '     <t/body>';
            str += ' </table>';
        }
        return $('#' + div + '').html(str);
    }

}
function llenarEntidadTitular(entidad) {
    let objResult = new Titular;
    // Persona
    objResult.IdEntidad = entidad.IdEntidad;
    objResult.NroAfiliado = entidad.NroAfiliado;
    objResult.ApellidoNombre = entidad.ApellidoNombre;
    objResult.NroDocumento = entidad.NroDocumento;
    objResult.IdSeccional = entidad.IdSeccional;

    objResult.CUIL = entidad.CUIL;
    objResult.CUITEmpresa = entidad.CUITEmpresa;
    objResult.RazonSocial = entidad.RazonSocial;
    objResult.Beneficios = entidad.Beneficios;

    objResult.ObjDomicilio = entidad.ObjDomicilio;
    return objResult;
}
