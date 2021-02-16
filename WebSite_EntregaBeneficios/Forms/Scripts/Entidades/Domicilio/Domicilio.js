class Domicilio {
    constructor() {
        this.IdEntidad = 0;
        this.Domicilio = '';
        this.IdLocalidad = 0;
        this.CodigoPostal = 0;
    }

    // static async TraerTodosXTitular(IdTitular) { 
    //     try {
    //         let entidad = "TipoBeneficios";
    //         let metodo = "TraerTodos";
    //         let url = ApiURL + "/" + entidad + "/" + metodo;
    //         let datos = await TraerAPI(url);
    //         let result = [];
    //         $.each(datos, function (key, value) {
    //             result.push(llenarEntidadDomicilio(value));
    //         });
    //         return result;
    //     } catch (error) {
    //         throw new Error(error);
    //     }

    // }
}