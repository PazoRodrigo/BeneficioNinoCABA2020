class Titular {
    constructor() {
        this.IdEntidad = 0;
        this.NroAfiliado = 0;
        this.ApellidoNombre = '';
        this.NroDocumento = 0;
        this.IdSeccional = 0;

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

}
function llenarEntidadTitular(entidad) {
    let objResult = new Titular;
    // Persona
    objResult.IdEntidad = entidad.IdEntidad;
    objResult.NroAfiliado = entidad.NroAfiliado;
    objResult.ApellidoNombre = entidad.ApellidoNombre;
    objResult.NroDocumento = entidad.NroDocumento;
    objResult.IdSeccional = entidad.IdSeccional;
    objResult.ObjDomicilio = entidad.ObjDomicilio;
    return objResult;
}
