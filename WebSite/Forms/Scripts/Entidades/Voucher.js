class Voucher {
  constructor() {
    this.IdEntidad = 0;
    this.IdTitular = 0;
    this.IdFamiliar = "";
    this.Codigo = "";
    this.Confirmado = 0;
    this.IdProvincia = 0;
    this.CP = 0;
    this.IdLocalidad = 0;
    this.Domicilio = '';
  }

  static async TraerUnoXId(idVoucher) {
      try {
        let data = {
            idVoucher: idVoucher,
        };
        let entidad = "Voucher";
        let metodo = "TraerUnoxId";
        let url = ApiURL + "/" + entidad + "/" + metodo;
        let datos = await TraerAPI(url, data);
        let result = [];
        $.each(datos, function (key, value) {
          result.push(llenarEntidad(value));
        });
        return result[0];
      } catch (error) {
          throw new Error(error)
      }
    
  }
  
  static async TraerTodosxAfiliado(idTitular) {
      try {
        let data = {
            idAfiliado: idTitular,
        };
        let entidad = "Voucher";
        let metodo = "TraerTodosxAfiliado";
        let url = ApiURL + "/" + entidad + "/" + metodo;
        let datos = await TraerAPI(url, data);
        let result = [];
        $.each(datos, function (key, value) {
          result.push(llenarEntidad(value));
        });
        return result;
      } catch (error) {
        throw new Error(error)
      }
    
  }
  static async Alta(V) {
    try {
        let data = V;
        let entidad = 'Voucher';
        let metodo = 'Alta';
        let url = ApiURL + '/' + entidad + '/' + metodo;
        return  await PostAPI(url, data);
    } catch (error) {
        throw new Error(error)
    }
  }
}
function llenarEntidad(entidad) {
  let objResult = new Voucher();
  objResult.IdEntidad = entidad.IdEntidad;
  objResult.IdTitular = entidad.IdTitular;
  objResult.IdFamiliar = entidad.IdFamiliar;
  objResult.Codigo = entidad.Codigo;
  objResult.Confirmado = entidad.Confirmado;
  objResult.Fecha = entidad.Fecha;
  objResult.IdProvincia = entidad.IdProvincia;
  objResult.CP = entidad.CP;
  objResult.IdLocalidad = entidad.IdLocalidad;
  objResult.Domicilio = entidad.Domicilio;
  return objResult;
}
