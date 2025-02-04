﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ninja.model.Entity;
using ninja.model.Manager;

namespace ninja.test {

    [TestClass]
    public class TestInvoice {

        [TestMethod]
        public void InsertNewInvoice() {

            InvoiceManager manager = new InvoiceManager();
            
            long id = 1006;
            Invoice invoice = new Invoice() {
                Id = id,
                Type = Invoice.Types.A.ToString(),

                #region "Por los cambios en el modelo"
                CustomerName = "Juan",
                PointOfSale = 1,
                Number = 1,
                Date = DateTime.Now               
                #endregion
            };

            #region "Por los cambios en el modelo"
            invoice.AddDetail(new InvoiceDetail
            {
                Amount = 1,
                Description = "Venta genérica",
                Id = 1,
                InvoiceId = id,
                UnitPrice = 100,
            });
            #endregion

            manager.Insert(invoice);
            Invoice result = manager.GetById(id);

            Assert.AreEqual(invoice, result);

        }

        [TestMethod]
        public void InsertNewDetailInvoice() {

            InvoiceManager manager = new InvoiceManager();
            long id = 10006;
            Invoice invoice = new Invoice() {
                Id = id,
                Type = Invoice.Types.A.ToString(),
                #region "Por los cambios en el modelo"
                CustomerName = "Juan",
                PointOfSale = 1,
                Number = 1,
                Date = DateTime.Now
                #endregion
            };

            invoice.AddDetail(new InvoiceDetail() {
                Id = id,
                InvoiceId = id,
                Description = "Venta insumos varios",
                Amount = 14,
                UnitPrice = 4.33
            });

            invoice.AddDetail(new InvoiceDetail() {
                Id = id,
                InvoiceId = 6,
                Description = "Venta insumos tóner",
                Amount = 5,
                UnitPrice = 87
            });

            manager.Insert(invoice);
            Invoice result = manager.GetById(id);

            Assert.AreEqual(invoice, result);

        }

        [TestMethod]
        public void DeleteInvoice() {

            /*
              1- Eliminar la factura con id=4
              2- Comprobar de que la factura con id=4 ya no exista
              3- La prueba tiene que mostrarse que se ejecuto correctamente
            */

            #region Escribir el código dentro de este bloque

            InvoiceManager manager = new InvoiceManager();

            if (!manager.Exists(4))
            {
                Invoice invoice = new Invoice()
                {
                    Id = 4,
                    Type = Invoice.Types.A.ToString(),
                    #region "Por los cambios en el modelo"
                    CustomerName = "Juan",
                    PointOfSale = 1,
                    Number = 1,
                    Date = DateTime.Now
                    #endregion
                };
                #region "Por los cambios en el modelo"
                invoice.AddDetail(new InvoiceDetail
                {
                    Amount = 1,
                    Description = "Venta genérica",
                    Id = 1,
                    InvoiceId = 4,
                    UnitPrice = 100,
                });
                #endregion
                manager.Insert(invoice);
            }

            Assert.IsTrue(manager.Exists(4));

            manager.Delete(4);

            Assert.IsFalse(manager.Exists(4));

            #endregion Escribir el código dentro de este bloque

        }

        [TestMethod]
        public void UpdateInvoiceDetail() {

            long id = 1003;
            InvoiceManager manager = new InvoiceManager();
            IList<InvoiceDetail> detail = new List<InvoiceDetail>();

            detail.Add(new InvoiceDetail() {
                Id = 1,
                InvoiceId = id,
                Description = "Venta insumos varios",
                Amount = 14,
                UnitPrice = 4.33
            });

            detail.Add(new InvoiceDetail() {
                Id = 2,
                InvoiceId = id,
                Description = "Venta insumos tóner",
                Amount = 5,
                UnitPrice = 87
            });

            manager.UpdateDetail(id, detail);
            Invoice result = manager.GetById(id);

            Assert.AreEqual(2, result.GetDetail().Count());

        }

        [TestMethod]
        public void CalculateInvoiceTotalPriceWithTaxes() {

            long id = 1005;
            InvoiceManager manager = new InvoiceManager();
            Invoice invoice = manager.GetById(id);

            double sum = 0;
            foreach(InvoiceDetail item in invoice.GetDetail()) 
                sum += item.TotalPrice * item.Taxes;

            Assert.AreEqual(sum, invoice.CalculateInvoiceTotalPriceWithTaxes());

        }

    }

}