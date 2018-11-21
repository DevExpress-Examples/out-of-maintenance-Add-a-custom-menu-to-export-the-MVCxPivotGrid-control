using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace MPG_ExportMenu.Models {
    public class NwindModel {
        public static DataTable GetInvoices() {
            string[] providers = new string[] { "Microsoft.Jet.OLEDB.4.0", "Microsoft.ACE.OLEDB.12.0", };
            foreach (string provider in providers) {
                try {
                    using (OleDbConnection connection = GetConnection(provider)) {
                        connection.Open();
                        return GetData(connection, "SELECT * FROM [Invoices]");
                    }
                }
                catch {
                }
            }

            return null;
        }

        static OleDbConnection GetConnection(string provider) {
            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = string.Format("Provider={0};Data Source={1}", provider, HttpContext.Current.Server.MapPath("~/App_Data/nwind.mdb"));
            return connection;
        }

        private static DataTable GetData(OleDbConnection connection, string command) {
            DataTable dataTableInvoices = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(string.Empty, connection);
            adapter.SelectCommand.CommandText = command;
            adapter.Fill(dataTableInvoices);
            return dataTableInvoices;
        }
    }


}
