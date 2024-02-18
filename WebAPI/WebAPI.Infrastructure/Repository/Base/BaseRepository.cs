using Dapper;
using WebAPI.Application;
using WebAPI.Domain;


namespace WebAPI.Infrastructure
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : IEntity
    {

        protected readonly IUnitOfWork Uow;
        public virtual string TableName { get; set; } = typeof(TEntity).Name;
        public BaseRepository(IUnitOfWork uow)
        {
            Uow = uow;

        }

        /// <summary>
        /// Hàm tìm kiếm Entity theo Id
        /// </summary>
        /// <param name="id">Định danh của Entity (Guid)</param>
        /// <returns>Thông tin Entity nếu thành công, null nếu thất bại</returns>
        /// Created by: nkmdang (19/09/2023)
        public async Task<TEntity> FindByIdAsync(Guid id)
        {
            // Tạo câu truy vấn
            var sql = TEntitySQL.GetByIdSQL(TableName, id);

            // Tạo param 
            var param = new DynamicParameters();
            param.Add($"{TableName}Id", id);

            var result = await Uow.Connection.QueryFirstOrDefaultAsync<TEntity>(sql, param, transaction: Uow.Transaction);
            return result;
        }

        /// <summary>
        /// Hàm lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Created by: nkmdang (19/09/2023)
        public async Task<List<TEntity>> GetFilterAsync(int page, int pageSize, string? property)
        {
            // Tạo câu truy vấn
            string sql = TEntitySQL.GetAllSQL(TableName);

            // Thực hiện truy vấn
            var result = await Uow.Connection.QueryAsync<TEntity>(sql, transaction: Uow.Transaction);
            return result.ToList();
        }

        /// <summary>
        /// Hàm lấy thông tin Entity theo Id
        /// </summary>
        /// <param name="id">Định danh của Entity (Guid)</param>
        /// <returns>Thông tin Entity nếu thành công, null nếu thất bại</returns>
        /// Created by: nkmdang (19/09/2023)
        public virtual async Task<TEntity> GetByIdAsync(Guid id)
        {
            var entity = await FindByIdAsync(id);   
            if(entity != null) 
            {
                return entity;
            } 
            else
            {
                throw new NotFoundException("Không tìm thấy tài nguyên", "Không tìm thấy tài nguyên", 404);
            }
        }


        /// <summary>
        /// Hàm lấy thông tin nhiều Entity theo Id
        /// </summary>
        /// <param name="ids">Định danh của các Entity (Guid)</param>
        /// <returns>Thông tin các Entity nếu thành công, null nếu thất bại</returns>
        /// Created by: nkmdang (20/09/2023)
        public async Task<List<TEntity>> GetByListIdAsync(List<Guid> ids)
        {
            // Tạo câu lệnh SQL (không truyền vào list ids)
            string sql = TEntitySQL.GetByListIdSQL(TableName);

            //Tạo param
            var param = new DynamicParameters();
            param.Add("ids", ids);

            // Truy vấn
            var entities = await Uow.Connection.QueryAsync<TEntity>(sql, param, transaction: Uow.Transaction);
            return entities.ToList();
        }

        /// <summary>
        /// Hàm thêm mới một Entity
        /// </summary>
        /// <param name="entity">Instance của Entity</param>
        /// <returns>Thông tin Entity đã thêm thành công</returns>
        /// Created by: nkmdang (20/09/2023)
        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            // Tạo câu lệnh SQL
            string sql;

            //if (entity is Employee)
            //{
            //    sql = EmployeeSQL.CreateOneEmployeeWithDepartmentIdSQL();
            //}
            //else if (entity is Department)
            //{
            //    sql = DepartmentSQL.CreateDepartmentSQL();
            //}
            //else sql = "";

            sql = TEntitySQL.InsertSQL(entity);

            //// Thực thi truy vấn
            var result = await Uow.Connection.QuerySingleOrDefaultAsync<TEntity>(sql, entity, transaction: Uow.Transaction   );
            return result;
        }

        /// <summary>
        /// Hàm thêm mới nhiều Entity
        /// </summary>
        /// <param name="entities">Instances của Entity</param>
        /// <returns>Thông tin các Entity đã thêm thành công</returns>
        /// Created by: nkmdang (20/09/2023)
        public virtual async Task<List<TEntity>> InsertManyAsync(List<TEntity> entities)
        {
            var firstEntity = entities.FirstOrDefault();
            if (firstEntity != null)
            {
                string sql = TEntitySQL.InsertSQL(firstEntity);
                var result = new List<TEntity>();
                foreach (var entity in entities)
                {
                    if(entity.GetId() == Guid.Empty)
                    {
                        entity.SetId(Guid.NewGuid());   
                    }   
                    var addSuccessEntity = await Uow.Connection.QueryAsync<TEntity>(sql, entity, transaction: Uow.Transaction);
                    if (addSuccessEntity != null)
                    {
                        result.Add(entity);
                    }
                }
                return result;
            }
            else return [];
        }


        /// <summary>
        /// Hàm sửa thông tin một Entity
        /// </summary>
        /// <param name="entity">Instance của Entity</param>
        /// <returns>Thông tin của Entity sau khi đã thay đổi</returns>
        /// Created by: nkmdang (20/09/2023)
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            // Tạo câu lệnh SQL
            string sql;

            sql = TEntitySQL.UpdateSQL(entity); 

            //// Thực thi truy vấn
            var result = await Uow.Connection.QueryFirstOrDefaultAsync<TEntity>(sql, entity, transaction: Uow.Transaction);
            Console.WriteLine(result);  
            return result;
        }

        /// <summary>
        /// Hàm xóa thông tin một Entity
        /// </summary>
        /// <param name="id">Định danh Entity</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: nkmdang (20/09/2023)
        public async Task<int> DeleteAsync(Guid id)
        {
            // Tạo câu lệnh SQL
            string sql = TEntitySQL.DeleteByIdSQL(TableName, id);
            var param = new DynamicParameters();
            param.Add("Id", id);
            var result = await Uow.Connection.ExecuteAsync(sql, param,  transaction: Uow.Transaction);
            return result;
        }

        /// <summary>
        /// Hàm xóa thông tin nhiều Entity
        /// </summary>
        /// <param name="ids">Danh sách các dịnh danh Entity</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by: nkmdang (20/09/2023)
        public async Task<int> DeleteManyAsync(List<TEntity> entities)
        {
            // Tạo câu lệnh SQL
            string sql = TEntitySQL.DeleteByListIdSQL(TableName);

            var param = new DynamicParameters();
            var ids = entities.Select(entity => entity.GetId()).ToList();
            param.Add("ids", ids);
            var result = await Uow.Connection.ExecuteAsync(sql, param, transaction: Uow.Transaction); 
            return result;
        }
    }
}
