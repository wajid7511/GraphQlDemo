using Identity.Abstractions;
using Identity.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Identity.Factory;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController(IIdentityManager identityManager, IMapper mapper) : ControllerBase
    {
        private readonly IIdentityManager _identityManager = identityManager ?? throw new ArgumentNullException(nameof(identityManager));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        [HttpPost]
        public async ValueTask<ApiResponseModel> CreateIdentity([FromBody] string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new ArgumentNullException(nameof(customerId));
            }
            var response = await _identityManager.CreateIdentityAsync(customerId);
            var result = _mapper.Map<IdentityCreateResponseModel>(response);
            return ApiResponseFactory.CreateSuccessResponse(result);
        }
    }
}
