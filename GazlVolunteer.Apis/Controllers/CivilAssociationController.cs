using GazlVolunteer.Core.Dtos;
using GazlVolunteer.Core.IRepositories;
using GazlVolunteer.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GazlVolunteer.Apis.Controllers
{
    public class CivilAssociationController : BaseApiController
    {
        private readonly ICivilAssociationRepository _civilAssociationRepository;

        public CivilAssociationController(ICivilAssociationRepository civilAssociationRepository)
        {
            _civilAssociationRepository = civilAssociationRepository;
        }

        /// <summary>
        /// استرجاع جميع الجمعيات الأهلية.
        /// </summary>
        /// <remarks>
        /// هذا الإجراء يقوم بجلب قائمة جميع الجمعيات الأهلية المتاحة في النظام.
        /// </remarks>
        /// <response code="200">تم جلب البيانات بنجاح.</response>
        /// <response code="400">طلب غير صالح.</response>
        [HttpGet("getAllCivilAssociations")]
        public async Task<IActionResult> GetAllCivilAssociations()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _civilAssociationRepository.GetAllCivilAssociations();
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// استرجاع تفاصيل جمعية أهلية معينة.
        /// </summary>
        /// <param name="CivilAssociationId">معرف الجمعية الأهلية المطلوب استرجاعها.</param>
        /// <remarks>
        /// هذا الإجراء يقوم بجلب تفاصيل جمعية أهلية معينة باستخدام معرفها.
        /// </remarks>
        /// <response code="200">تم جلب بيانات الجمعية بنجاح.</response>
        /// <response code="400">طلب غير صالح أو الجمعية غير موجودة.</response>
        [HttpGet("getCivilAssociation")]
        public async Task<IActionResult> GetCivilAssociation(int CivilAssociationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _civilAssociationRepository.Get(CivilAssociationId);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// إضافة جمعية أهلية جديدة.
        /// </summary>
        /// <param name="model">بيانات الجمعية الأهلية الجديدة.</param>
        /// <remarks>
        /// هذا الإجراء يقوم بإضافة جمعية أهلية جديدة. يجب توفير البيانات المطلوبة لإتمام العملية.
        /// </remarks>
        /// <response code="200">تمت إضافة الجمعية بنجاح.</response>
        /// <response code="400">البيانات غير صالحة أو الطلب غير مكتمل.</response>
        [HttpPost("addCivilAssociation")]
        public async Task<IActionResult> AddCivilAssociation([FromBody] CivilAssociationsDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _civilAssociationRepository.Add(model);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// تحديث بيانات جمعية أهلية.
        /// </summary>
        /// <param name="CivilAssociationId">معرف الجمعية الأهلية المطلوب تحديثها.</param>
        /// <param name="model">البيانات الجديدة للجمعية الأهلية.</param>
        /// <remarks>
        /// هذا الإجراء يقوم بتحديث بيانات جمعية أهلية موجودة باستخدام معرفها.
        /// </remarks>
        /// <response code="200">تم تحديث بيانات الجمعية بنجاح.</response>
        /// <response code="400">البيانات غير صالحة أو الجمعية غير موجودة.</response>
        [HttpPut("updateCivilAssociation")]
        public async Task<IActionResult> UpdateCivilAssociation(int CivilAssociationId, [FromBody] CivilAssociationsDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _civilAssociationRepository.Update(CivilAssociationId, model);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        /// <summary>
        /// حذف جمعية أهلية.
        /// </summary>
        /// <param name="CivilAssociationId">معرف الجمعية الأهلية المطلوب حذفها.</param>
        /// <remarks>
        /// هذا الإجراء يقوم بحذف جمعية أهلية باستخدام معرفها.
        /// </remarks>
        /// <response code="200">تم حذف الجمعية بنجاح.</response>
        /// <response code="400">طلب غير صالح أو الجمعية غير موجودة.</response>
        [HttpDelete("deleteCivilAssociation")]
        public async Task<IActionResult> DeleteCivilAssociation(int CivilAssociationId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _civilAssociationRepository.Delete(CivilAssociationId);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }

}
