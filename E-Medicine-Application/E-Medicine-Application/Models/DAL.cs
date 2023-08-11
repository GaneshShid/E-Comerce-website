using Microsoft.Data.SqlClient;

using System.Data;
namespace E_Medicine_Application.Models

{

    // here we use all the methods connect with database 
    // database logic using ado.net.
    public class DAL
    {

        public Response register(Users users,SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_register", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Password", users.Password);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Fund", 0);
            cmd.Parameters.AddWithValue("@Type", "Users");
            cmd.Parameters.AddWithValue("@Type ","Pending");
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if(i>0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "user registered succesfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "user registeration failed";
            }
            return response;
        }

        public Response login(Users users, SqlConnection connection) 
        {
            SqlDataAdapter da = new SqlDataAdapter("sp_login", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@Email", users.Email);
            da.SelectCommand.Parameters.AddWithValue("@Password", users.Password);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if(dt.Rows.Count>0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]);
                response.StatusCode = 200;
                response.StatusMessage = "User is Valid";
                response.user = user;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User is Not Valid";
                response.user = null;
            }
            return response;
        }

        public Response viewUser(Users users,SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("p_viewUser", connection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@ID", users.ID);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            Users user = new Users();
            if (dt.Rows.Count>0)
            {
                user.ID = Convert.ToInt32(dt.Rows[0]["ID"]);
                user.FirstName = Convert.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Convert.ToString(dt.Rows[0]["LastName"]);
                user.Email = Convert.ToString(dt.Rows[0]["Email"]);
                user.Type = Convert.ToString(dt.Rows[0]["Type"]); 
                user.Fund = Convert.ToDecimal(dt.Rows[0]["Fund"]); 
                user.CreatedON = Convert.ToDateTime(dt.Rows[0]["CreatedON"]); 
                user.Password = Convert.ToString(dt.Rows[0]["Password"]); 
                response.StatusCode = 200;
                response.StatusMessage = "user exist";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User does not exist";
                response.user = user;
            }
            return response;
        }

        public Response updateProfile(Users users,SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_updateProfile",connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FirstName", users.FirstName);
            cmd.Parameters.AddWithValue("@LastName", users.LastName);
            cmd.Parameters.AddWithValue("@Email", users.Email);
            cmd.Parameters.AddWithValue("@Password", users.Password);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if(i>0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "record updated succesfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "record not updated";
            }
            return response;
        }



        public Response addToCart(Cart cart,SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_AddToCart", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserId", cart.UserId);
            cmd.Parameters.AddWithValue("@UnitPrice", cart.UnitPrice);
            cmd.Parameters.AddWithValue("@Discount", cart.Discount);
            cmd.Parameters.AddWithValue("@Quantity", cart.Quantity);
            cmd.Parameters.AddWithValue("@TotalPrice", cart.TotalPrice);
            cmd.Parameters.AddWithValue("@MedicineId", cart.MedicineId);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if(i>0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Item added succesfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Item not added";
            }
            return response;
        }   

        public Response placeOrder(Users users,SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("sp_placeOrder", connection);
            cmd.Parameters.AddWithValue("@ID",users.ID);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();
            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Order has been placed succesfully";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Order could not been placed";
            }
            return response;

        }

    }
}
