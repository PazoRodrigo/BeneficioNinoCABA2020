class Voucher {
  constructor() {
    this.IdEntidad = 0;
    this.IdTitular = 0;
    this.IdFamiliar = "";
    this.Codigo = "";
    this.Confirmado = 0;
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
        result.push(llenarEntidadVoucher(value));
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
        result.push(llenarEntidadVoucher(value));
      });
      return result;
    } catch (error) {
      throw new Error(error)
    }

  }
  static async Guardar(ListaBeneficios, ObjDomicilio) {
    try {
      spinner();
      let i = 0;
      while (i <= ListaBeneficios.length - 1) {
        let ObjVoucher = new Voucher;
        ObjVoucher.IdTitular = ListaBeneficios[i].NroAfiliado;
        ObjVoucher.IdFamiliar = ListaBeneficios[i].IdEntidad;
        await Voucher.Alta(ObjVoucher, ObjDomicilio);
        i++;
      }
      spinnerClose();
      let TotalGenerados = i;
      let TextoOK = '';
      if (TotalGenerados == 1) {
        TextoOK = 'Se ha generado 1 voucher';
      } else {
        TextoOK = 'Se han generado ' + i + ' vouchers.'
      }
      alertOk(TextoOK);
    } catch (error) {
      spinnerClose();
      alertAlerta(error);
    }
  }
  static async Alta(entidadVoucher, ObjDomicilio) {
    try {
      let data = {
        'voucher': entidadVoucher,
        'ObjDomicilio': ObjDomicilio
      }
      let entidad = 'Voucher';
      let metodo = 'Alta';
      let url = ApiURL + '/' + entidad + '/' + metodo;
      return await PostAPI(url, data);
    } catch (error) {
      throw new Error(error)
    }
  }
}
function llenarEntidadVoucher(entidad) {
  let objResult = new Voucher();
  objResult.IdEntidad = entidad.IdEntidad;
  objResult.IdTitular = entidad.IdTitular;
  objResult.IdFamiliar = entidad.IdFamiliar;
  objResult.Codigo = entidad.Codigo;
  objResult.Confirmado = entidad.Confirmado;
  objResult.Fecha = entidad.Fecha;
  return objResult;
}
