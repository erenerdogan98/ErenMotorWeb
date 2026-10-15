using AutoMapper;
using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.BLL.Helper;
using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.Dto.ServiceResponseDto;
using ErenMotorWeb.Entities;
using Serilog;
using System.Linq.Expressions;


namespace ErenMotorWeb.BLL.Concrete
{
    public class GenericManager<T, TCreateDto, TUpdateDto, TResultDto>(IGenericDAL<T> genericDAL, IMapper mapper) : IGenericService<TCreateDto, TUpdateDto, TResultDto> where T : class, IEntityBase, new() where TCreateDto : class
    {
        private static string EntityName => typeof(T).Name; // for using logging.
        public virtual async Task<ApiResponseDto<string>> TAddAsync(TCreateDto createDto)
        {
            try
            {
                if (createDto == null)
                    return new ApiResponseDto<string>(EntityName, false, 400, "Bad Request!");
                var entity = mapper.Map<T>(createDto);
                await genericDAL.AddAsync(entity);
                Log.Information($"{EntityName} added successfully");
                return new ApiResponseDto<string>(entity.Id.ToString(), true, 201, $"{EntityName} added successfully");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error occured while creating {EntityName}");
                return new ApiResponseDto<string>(EntityName, false, 500, ex.Message);
            }
        }

        public async Task<ApiResponseDto<string>> TDeleteAsync(int id)
        {
            try
            {
                var values = await genericDAL.GetByIdAsync(id);
                if (values != null)
                {
                    await genericDAL.DeleteAsync(id);
                    Log.Information($"{EntityName} deleted successfully with id : {id}");
                    return new ApiResponseDto<string>(null, true, 200, $"{EntityName} deleted successfully");
                }
                return new ApiResponseDto<string>(null, false, 404, $"{EntityName} not found");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occured while deleting {EntityName} by Id : {id}");
                return new ApiResponseDto<string>(null, false, 500, ex.Message);
            }
        }

        public virtual async Task<ApiResponseDto<IEnumerable<TResultDto>>> TGetAllAsync()
        {
            try
            {
                var entities = await genericDAL.GetAllAsync();
                if (entities != null)
                {
                    var entitiesDto = mapper.Map<IEnumerable<TResultDto>>(entities);
                    return new ApiResponseDto<IEnumerable<TResultDto>>(entitiesDto, true, 200, $"{EntityName} values fetched successfully");
                }
                return new ApiResponseDto<IEnumerable<TResultDto>>(null, false, 400, $"{EntityName} values not found");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occured while fetching values {EntityName}");
                return new ApiResponseDto<IEnumerable<TResultDto>>(null, false, 500, ex.Message);
            }
        }
        public async Task<ApiResponseDto<IEnumerable<TResultDto>>> TGetAllAsync(
    Expression<Func<TResultDto, bool>> filter)
        {
            try
            {
                var expression =
                    FilterExpressionConverter<T, TResultDto>.Convert(filter);

                var entities =
                    await genericDAL.GetAllAsync(expression);

                var entitiesDto =
                    mapper.Map<IEnumerable<TResultDto>>(entities);

                return new ApiResponseDto<IEnumerable<TResultDto>>(
                    entitiesDto,
                    true,
                    200,
                    $"{EntityName} values fetched successfully by filter.");
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    $"An error occured while fetching values {EntityName} by filter");

                return new ApiResponseDto<IEnumerable<TResultDto>>(
                    null,
                    false,
                    500,
                    ex.Message);
            }
        }
        //public async Task<ApiResponseDto<IEnumerable<TResultDto>>> TGetAllAsync(Expression<Func<TResultDto, bool>> filter)
        //{
        //    try
        //    {
        //        var expression = FilterExpressionConverter<T, TResultDto>.Convert(filter);
        //        var entities = await genericDAL.GetAllAsync(expression);
        //        if (entities != null)
        //        {
        //            var entitiesDto = mapper.ProjectTo<TResultDto>(entities.AsQueryable()).ToList();
        //            return new ApiResponseDto<IEnumerable<TResultDto>>(entitiesDto, true, 200, $"{EntityName} values fetched successfully by filter.");
        //        }
        //        return new ApiResponseDto<IEnumerable<TResultDto>>(null, false, 400, $"{EntityName} values not found by filter.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex, $"An error occured while fetching values {EntityName} by filter");
        //        return new ApiResponseDto<IEnumerable<TResultDto>>(null, false, 500, ex.Message);
        //    }
        //}

        public ApiResponseDto<TResultDto> TGetById(int id)
        {
            try
            {
                if (id <= 0)
                    return new ApiResponseDto<TResultDto>(default, false, 422, "Invalid Id");
                var entity = genericDAL.GetById(id);
                if (entity != null)
                {
                    var entityDto = mapper.Map<TResultDto>(entity);
                    return new ApiResponseDto<TResultDto>(entityDto, true, 200, $"{EntityName} values fecthed successfully by Id");
                }
                return new ApiResponseDto<TResultDto>(default, false, 400, $"{EntityName} values not found by Id");

            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occured while fetching data : {EntityName} by Id : {id}");
                return new ApiResponseDto<TResultDto>(default, false, 500, $"An error occured while fetching data : {EntityName} by Id");
            }
        }

        public virtual async Task<ApiResponseDto<TResultDto>> TGetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                    return new ApiResponseDto<TResultDto>(default, false, 422, "Invalid Id");
                var entity = await genericDAL.GetByIdAsync(id);
                if (entity != null)
                {
                    var entityDto = mapper.Map<TResultDto>(entity);
                    return new ApiResponseDto<TResultDto>(entityDto, true, 200, $"{EntityName} values fecthed successfully by Id");
                }
                return new ApiResponseDto<TResultDto>(default, false, 400, $"{EntityName} values not found by Id");

            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occured while fetching data : {EntityName} by Id : {id}");
                return new ApiResponseDto<TResultDto>(default, false, 500, $"An error occured while fetching data : {EntityName} by Id");
            }
        }

        public async Task<ApiResponseDto<TResultDto>> TGeyByFilterAsync(Expression<Func<TResultDto, bool>> filter)
        {
            try
            {
                var expression = FilterExpressionConverter<T, TResultDto>.Convert(filter);
                var entities = await genericDAL.GetByFilter(expression);
                if (entities != null)
                {
                    var entitiesDto = mapper.Map<TResultDto>(entities);
                    return new ApiResponseDto<TResultDto>(entitiesDto, true, 200, $"{EntityName} values fetched successfully by filter.");
                }
                return new ApiResponseDto<TResultDto>(default, false, 400, $"{EntityName} values not found by filter.");
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"An error occured while fetching values {EntityName} by filter");
                return new ApiResponseDto<TResultDto>(default, false, 500, ex.Message);
            }
        }

        public virtual async Task<ApiResponseDto<string>> TUpdateAsync(TUpdateDto updateDto)
        {
            try
            {
                if (updateDto == null)
                {
                    return new ApiResponseDto<string>(
                        EntityName,
                        false,
                        400,
                        "Bad Request!");
                }

                var entity = mapper.Map<T>(updateDto);

                await genericDAL.UpdateAsync(entity);

                return new ApiResponseDto<string>(
                    EntityName,
                    true,
                    200,
                    $"{EntityName} updated successfully");
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    $"Some issues happened while updating {EntityName} entity");

                return new ApiResponseDto<string>(
                    EntityName,
                    false,
                    500,
                    ex.Message);
            }
        }
    }
}
