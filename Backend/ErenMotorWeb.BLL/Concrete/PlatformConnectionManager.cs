using AutoMapper;
using ErenMotorWeb.BLL.Abstract;
using ErenMotorWeb.DAL.Abstract;
using ErenMotorWeb.Dto.Enums;
using ErenMotorWeb.Dto.PlatformConnectionDto;
using ErenMotorWeb.Dto.ServiceResponseDto;
using ErenMotorWeb.Entities;
using ErenMotorWeb.Entities.Enums;
using Serilog;
using Microsoft.AspNetCore.DataProtection;
using ErenMotorWeb.BLL.Helper;

namespace ErenMotorWeb.BLL.Concrete
{
    public class PlatformConnectionManager(
        IPlatformConnectionDAL platformConnectionDAL,
        IMapper mapper, PlatformCredentialProtector credentialProtector)
        : GenericManager<
            PlatformConnection,
            CreatePlatformConnectionDto,
            UpdatePlatformConnectionDto,
            ResultPlatformConnectionDto>(
                platformConnectionDAL,
                mapper),
          IPlatformConnectionService
    {

        public override async Task<ApiResponseDto<string>> TAddAsync(
           CreatePlatformConnectionDto dto)
        {
            try
            {
                var entity = mapper.Map<PlatformConnection>(dto);

                entity.ApiKeyEncrypted =
                    credentialProtector.Protect(dto.ApiKey);

                entity.ApiSecretEncrypted =
                    credentialProtector.Protect(dto.ApiSecret);

                await platformConnectionDAL.AddAsync(entity);

                return new ApiResponseDto<string>(
                    "Platform connection added successfully.",
                    true,
                    200,
                    "Platform connection added successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(
                    ex,
                    "An error occurred while adding platform connection.");

                return new ApiResponseDto<string>(
                    null,
                    false,
                    500,
                    ex.Message);
            }
        }

        public async Task<ApiResponseDto<IEnumerable<ResultPlatformConnectionDto>>>
            TGetActiveConnectionsAsync()
        {
            try
            {
                var connections =
                    await platformConnectionDAL.GetActiveConnectionsAsync();

                var values =
                    mapper.Map<IEnumerable<ResultPlatformConnectionDto>>(connections);

                return new ApiResponseDto<IEnumerable<ResultPlatformConnectionDto>>(
                    values,
                    true,
                    200,
                    "Active platform connections fetched successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "An error occurred while fetching active platform connections.");

                return new ApiResponseDto<IEnumerable<ResultPlatformConnectionDto>>(
                    null,
                    false,
                    500,
                    ex.Message);
            }
        }

        public async Task<ApiResponseDto<ResultPlatformConnectionDto>>
            TGetByPlatformTypeAsync(PlatformTypeDto platformType)
        {
            try
            {
                var entityPlatformType =
                    mapper.Map<PlatformType>(platformType);

                var connection =
                    await platformConnectionDAL
                        .GetByPlatformTypeAsync(entityPlatformType);

                if (connection == null)
                {
                    return new ApiResponseDto<ResultPlatformConnectionDto>(
                        default,
                        false,
                        404,
                        "Platform connection not found.");
                }

                var value =
                    mapper.Map<ResultPlatformConnectionDto>(connection);

                return new ApiResponseDto<ResultPlatformConnectionDto>(
                    value,
                    true,
                    200,
                    "Platform connection fetched successfully.");
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "An error occurred while fetching platform connection.");

                return new ApiResponseDto<ResultPlatformConnectionDto>(
                    default,
                    false,
                    500,
                    ex.Message);
            }
        }
    }
}