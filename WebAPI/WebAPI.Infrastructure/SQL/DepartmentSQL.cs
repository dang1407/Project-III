namespace WebAPI
{
    public class DepartmentSQL
    {
        /// <summary>
        /// Hàm tạo câu lệnh SQL lấy department theo DepartmentName
        /// </summary>
        /// <param name="departmentName">Tên đơn vị</param>
        /// <returns>Câu lệnh SQL</returns>
        /// Created by: nkmdang (21/09/2023)
        public static string GetDepartmentByNameSQL(string departmentName)
        {
            string sql = $"SELECT * FROM Department WHERE DepartmentName = '{departmentName}'";
            return sql;
        }


        /// <summary>
        /// Tạo câu lệnh SQL thêm mới Đơn vị
        /// </summary>
        /// <returns></returns>
        /// Created by: nkmdang (25/09/2023)
        public static string CreateDepartmentSQL()
        {
            string sql = "";
            return sql;
        }


        /// <summary>
        /// Tạo câu lệnh SQL sửa Đơn vị
        /// </summary>
        /// <returns></returns>
        /// Created by: nkmdang (25/09/2023)
        public static string UpdateDepartmentSQL()
        {
            string sql = "";
            return sql;
        }
    }
}
