class Familiar {
    constructor() {
        this.IdEntidad = 0;
        this.IdAfiliadoTitular = 0;
        this.NroAfiliado = 0;
        this.ApellidoNombre = '';
        this.NroDocumento = 0;
        this.FechaNacimiento = 0;
        this.Edad = 0;
    }

    static async TraerTodosXTitular(IdAfiliado) {
        let data = {
            "IdTitular": IdAfiliado
        };
        let entidad = 'Familiares';
        let metodo = 'TraerTodosXTitular';
        let url = ApiURL + '/' + entidad + '/' + metodo;
        let datos = await TraerAPI(url, data);
        let result = [];
        $.each(datos, function (key, value) {
            result.push(llenarEntidadFamiliar(value));
        });
        return result;
    }

    static async ArmarGrilla(div, lista, evento) {
        $('#' + div + '').html('');
        let str = '';
        if (lista.length > 0) {
            str += ' <table class="table table-bordered">';
            str += '     <thead>';
            str += '         <tr>';
            str += '             <th></th>'; //Seleccion
            str += '             <th class="text-center">Nombre</th>';
            str += '             <th class="text-center">Fecha Nacimiento</th>';
            str += '             <th class="text-center">Nro. Documento</th>';
            str += '             <th class="text-center">Edad</th>';
            str += '         </tr>';
            str += '     </thead>';
            str += '     <tbody>';
            for (let item of lista) {
                let habilitado = '';
                if (item.Edad > 12) {
                    habilitado = 'readonly="readonly"';
                }
                let seleccion = '';
                str += '    <tr>';
                str += '        <td ' + habilitado + '> ' + seleccion + '</td>';
                str += '        <td class="text-left pl-1"> ' + item.ApellidoNombre + '</td>';
                str += '        <td class="text-center"> ' + LongToDateString(item.FechaNacimiento) + '</td>';
                str += '        <td class="text-center"> ' + Right('00000000' + item.NroDocumento, 8) + '</td>';
                str += '        <td class="text-right pr-4"> ' + item.Edad + '</td>';
                str += '    </tr>';
            }
            str += '     <t/body>';
            str += ' </table>';
        }
        return $('#' + div + '').html(str);
    }
}
function llenarEntidadFamiliar(entidad) {
    let objResult = new Titular;
    // Persona
    objResult.IdEntidad = entidad.IdEntidad;
    objResult.IdAfiliadoTitular = entidad.IdAfiliadoTitular;
    objResult.NroAfiliado = entidad.NroAfiliado;
    objResult.ApellidoNombre = entidad.ApellidoNombre;
    objResult.NroDocumento = entidad.NroDocumento;
    objResult.FechaNacimiento = entidad.FechaNacimiento;
    objResult.Edad = entidad.Edad;
    return objResult;
}



