using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ninja.model.Entity;
using ninja.model.Mock;

namespace ninja.model.Manager {

    public class InvoiceManager {

        private InvoiceMock _mock;

        public InvoiceManager() {

            this._mock = InvoiceMock.GetInstance();

        }

        public IList<Invoice> GetAll() {

            return this._mock.GetAll();

        }

        public Invoice GetById(long id) {

            return this._mock.GetById(id);

        }

        public void Insert(Invoice item) {

            item.DoValidate();
            this._mock.Insert(item);
        }

        public void Delete(long id) {

            Invoice invoice = this.GetById(id);
            this._mock.Delete(invoice);

        }

        public Boolean Exists(long id) {

            return this._mock.Exists(id);

        }

        /// <summary>
        /// Updates the detail.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="detail">The detail.</param>
        /// <exception cref="Exception">The invoice doesn't exists (id {id}).</exception>
        public void UpdateDetail(long id, IList<InvoiceDetail> detail) {

            /*
              Este método tiene que reemplazar todos los items del detalle de la factura
              por los recibidos por parámetro
             */

            #region Escribir el código dentro de este bloque

            if (this.Exists(id))
            {
                Invoice _invoice = GetById(id);
                _invoice.DeleteDetails();
                detail.ToList().ForEach(x =>
                {
                    _invoice.AddDetail(x);
                });
            }
            else
            {
                throw new Exception($"The invoice doesn't exists (id {id}).");
            }

            #endregion Escribir el código dentro de este bloque

        }

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        public void Update(Invoice model)
        {
            Invoice _invoice = GetById(model.Id);
            _invoice.Update(model);
        }

    }

}