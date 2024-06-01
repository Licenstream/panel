using Domain;
using Domain.Interfaces;
using MySql.Data.MySqlClient;

namespace InfrastructureMySqlCommand;

public class LicenseCommandDataHandler : IDataBulkHandler<Domain.License>
{
    private readonly string _connectionString;

    public LicenseCommandDataHandler(string _connectionString)
    {
        this._connectionString = _connectionString;
    }

    public void InsertBulk(IEnumerable<License> dataTypes, int customerId)
    {
        string connStr = _connectionString;
        MySqlConnection conn = new MySqlConnection(connStr);
        try
        {
            conn.Open();

            var newEfLicenseList = SaveLicenses(conn, dataTypes, customerId);
            
        }
        catch (Exception ex)
        {
            conn.Close();

            throw;
        }
    }
    
    private IEnumerable<Domain.License> SaveLicenses(MySqlConnection conn, IEnumerable<License> licenses, int customerId)
    {
        foreach (var item in licenses)
        {
            string sql =
                $"INSERT INTO Panel.License (SkuId,Status,Name,TotalLicenses," +
                $"CreatedDate,NextLifeCycleDate,IsTrail) VALUES " +
                $"('{item.SkuId}','{item.Status}','{item.Name}'," +
                $"'{item.TotalLicenses}',@CreatedDate," +
                $"@NextLifeCycleDate,'{item.IsTrail}')";
            
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@CreatedDate", item.CreatedDate);
            cmd.Parameters.AddWithValue("@NextLifeCycleDate", item.NextLifeCycleDate);
            cmd.ExecuteNonQuery();

            if (cmd.LastInsertedId != null)
                cmd.Parameters.Add(new MySqlParameter("newId", cmd.LastInsertedId));

            var newLicenseId = Convert.ToInt32(cmd.Parameters["@newId"].Value);
            item.SetId(newLicenseId);
        }

        foreach (var item in licenses)
        {
            foreach (var serviceStatus in item.ServiceStats)
            {
                string sql =
                    $"INSERT INTO Panel.ServiceStatus (ServicePlanId, ServicePlanName, " +
                    $"ProvisioningStatus, AppliesTo) VALUES " +
                    $"('{serviceStatus.ServicePlanId}','{serviceStatus.ServicePlanName}'," +
                    $"'{serviceStatus.ProvisioningStatus}','{serviceStatus.AppliesTo}')";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                if (cmd.LastInsertedId != null)
                    cmd.Parameters.Add(new MySqlParameter("newId", cmd.LastInsertedId));

                var newServiceStatusId = Convert.ToInt32(cmd.Parameters["@newId"].Value);
                serviceStatus.SetId(newServiceStatusId);
            }
        }

        foreach (var item in licenses)
        {
            foreach (var serviceStatus in item.ServiceStats)
            {
                string sql =
                    $"INSERT INTO Panel.LicenseServiceStatus (LicenseId,ServiceStatusId) VALUES " +
                    $"({item.Id},{serviceStatus.Id})";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
            }
        }
        
        foreach (var item in licenses)
        {
                string sql =
                    $"INSERT INTO Panel.CustomerLicense (CustomerId,LicenseId) VALUES " +
                    $"({customerId},{item.Id})";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
        }

        return licenses;
    }
}