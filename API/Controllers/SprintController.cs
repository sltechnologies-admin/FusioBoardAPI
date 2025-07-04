using Microsoft.AspNetCore.Mvc;
using API.Common.Logging;
using API.Constants;
using API.Data.Interfaces;
using API.Features.Sprints.Common;
using API.Services.Interfaces;

namespace API.Controllers
{
    public class SprintController : BaseController
    {
        private readonly ISprintService _service;
        private readonly ISqlLogger _sqlLogger;
        private readonly IAppLogger<SprintController> _logger;

        public SprintController(ISprintService service, ISqlLogger sqlLogger, IAppLogger<SprintController> logger)
        {
            _service = service;
            _sqlLogger = sqlLogger;
            _logger = logger;
        }

        /// <summary>
        /// Create sprint: Purpose | Start a sprint
        /// </summary>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] SprintCreateDto dto)
        {
            const string eventCode = "SPRT-CRT-01";
            const string userMessage = Messages.Sprint.e_SprintCreateFailed;

            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { message = "Sprint name is required." });

            try
            {
                int userId = 1; // Replace with actual context
                var result = await _service.CreateAsync(dto, userId);

                if (!result.IsSccess)
                {
                    var errorMessage = result.UserErrorMessage ?? userMessage;
                    await LogHelper.LogErrorAsync(_sqlLogger, eventCode, CorrelationId, result.UserErrorMessage ?? userMessage, result.TechnicalErrorDetails);
                    return BadRequest(new { message = result.UserErrorMessage });
                }

                _logger.LogInformation("[{EventCode}] CorrelationId: {CorrelationId} - Sprint created: {SprintName}",
                    eventCode, CorrelationId, dto.Name);

                return Ok(new { message = Messages.Sprint.s_SprintCreated, sprintId = result.Data });
            }
            catch (Exception ex)
            {
                await LogHelper.LogErrorAsync(_sqlLogger, eventCode, CorrelationId, userMessage, ex.ToString());
                return StatusCode(HttpStatusCodes.InternalServerError, new { message = userMessage, correlationId = CorrelationId });
            }
        }

        /// <summary>
        /// Update sprint | Purpose: Edit name, dates
        /// </summary>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] SprintUpdateDto dto)
        {
            const string eventCode = "SPRT-UPD-01";
            const string userMessage = Messages.Sprint.e_SprintUpdateFailed;

            try
            {
                int userId = 1; // Replace with actual user context
                var result = await _service.UpdateAsync(dto, userId);
                //Need to show information, when Sprint Id not exist in db. showing only Success  - will confuse user

                if (!result.IsSccess)
                {
                    return await HandleFailureAsync(eventCode, result.UserErrorMessage, result.TechnicalErrorDetails);
                }

                return Ok(new { message = Messages.Sprint.s_SprintUpdatedSuccessfully });
            }
            catch (Exception ex)
            {
                return await HandleFailureAsync(eventCode, userMessage, ex.ToString());
            }
        }

        /// <summary>
        /// Get all sprints for a project | Purpose: List sprints
        /// </summary>
        [HttpGet("project/{projectId}")]
        public async Task<IActionResult> GetAllByProjectId(int projectId)
        {
            const string eventCode = "SPRT-FETCH-PROJ";
            const string userMessage = Messages.Sprint.e_SprintFetchFailed;

            try
            {
                var result = await _service.GetAllByProjectIdAsync(projectId);

                if (!result.IsSccess)
                    return await HandleFailureAsync(eventCode, result.UserErrorMessage, result.TechnicalErrorDetails);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return await HandleFailureAsync(eventCode, userMessage, ex.ToString());
            }
        }

        /// <summary>
        /// Get sprint by ID | Purpose: View sprint
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            const string eventCode = "SPRT-GET-ID";
            const string userMessage = Messages.Sprint.e_SprintFetchFailed;

            try
            {
                var result = await _service.GetByIdAsync(id);

                if (!result.IsSccess)
                    return await HandleFailureAsync(eventCode, result.UserErrorMessage, result.TechnicalErrorDetails);

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return await HandleFailureAsync(eventCode, userMessage, ex.ToString());
            }
        }

        /// <summary>
        /// Delete sprint(Sfot) | Purpose: To hide deleted Sprint from User
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            const string eventCode = "SPRT-DEL-01";
            const string userMessage = Messages.Sprint.e_SprintDeleteFailed;

            try
            {
                int userId = 1; // Replace with actual user context
                var result = await _service.DeleteAsync(id, userId);

                if (!result.IsSccess)
                    return await HandleFailureAsync(eventCode, result.UserErrorMessage, result.TechnicalErrorDetails);

                return Ok(new { message = Messages.Sprint.s_SprintDeleted });
            }
            catch (Exception ex)
            {
                return await HandleFailureAsync(eventCode, userMessage, ex.ToString());
            }
        }
    }
}
