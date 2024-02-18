using WebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Application
{
    public abstract class BaseReadOnlyService<TEntity, TDTO> : IBaseReadOnlyService<TDTO>
    {
        protected readonly IBaseRepository<TEntity> BaseRepository;

        protected BaseReadOnlyService(IBaseRepository<TEntity> baseRepository)
        {
            BaseRepository = baseRepository;
        }



        /// <summary>
        /// Hàm lấy ra tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// Created by: nkmdang (20/09/2023)
        public virtual async Task<List<TDTO>> GetFilterAsync(int page, int pageSize, string? property)
        {
            var entities = await BaseRepository.GetFilterAsync(page, pageSize, property);
            var result = entities.Select(entity => MapEntityToDTO(entity)).ToList();
            return result;
        }


        /// <summary>
        /// Hàm tìm kiếm Entity theo Id
        /// </summary>
        /// <param name="id">Định danh của Entity (Guid)</param>
        /// <returns>Thông tin Entity nếu thành công, null nếu thất bại</returns>
        /// Created by: nkmdang (20/09/2023)
        public virtual async Task<TDTO> FindByIdAsync(Guid id)
        {
            var entity = await BaseRepository.FindByIdAsync(id);
            var result = MapEntityToDTO(entity);
            return result;
        }

        /// <summary>
        /// Hàm lấy thông tin Entity theo Id
        /// </summary>
        /// <param name="id">Định danh của Entity (Guid)</param>
        /// <returns>Thông tin Entity nếu thành công, null nếu thất bại</returns>
        /// Created by: nkmdang (20/09/2023)
        public virtual async Task<TDTO> GetByIdAsync(Guid id)
        {
            var entity = await BaseRepository.GetByIdAsync(id); 
            var result = MapEntityToDTO(entity);    
            return result;  
        }


        /// <summary>
        /// Hàm chuyển đổi từ Entity sang DTO
        /// </summary>
        /// <param name="entity">Instance của TEntity</param>
        /// <returns>DTO</returns>
        /// Created by: nkmdang (19/09/2023)
        public abstract TDTO MapEntityToDTO(TEntity entity);
    }
}
