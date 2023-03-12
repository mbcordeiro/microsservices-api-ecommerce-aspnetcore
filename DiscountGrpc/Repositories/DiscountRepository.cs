using Dapper;
using DiscountGrpc.Entities;
using Npgsql;

namespace DiscountGrpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            var connection = GetConnectionPostgresSql();
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName = @ProductName", 
                new { ProductName = productName });
            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var connection = GetConnectionPostgresSql();
            var affected = await connection.ExecuteAsync
                ("INSERT INTO Cupon (ProductName, Description, Amount) " +
                "VALUES (@ProductName, @Description, @Amount)", 
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var connection = GetConnectionPostgresSql();
            var affected = await connection.ExecuteAsync
                ("UPDATE Cupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id = @Id",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, coupon.Id });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var connection = GetConnectionPostgresSql();
            var affected = await connection.ExecuteAsync("DELETE FROM Cupon WHERE ProductName=@ProductName",
                new { ProductName = productName });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        private NpgsqlConnection GetConnectionPostgresSql()
        {
            return new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
        }
    }
}
