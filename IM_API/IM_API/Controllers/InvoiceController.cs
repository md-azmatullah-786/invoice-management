using IM_API.Models;
using IM_API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        public InvoiceController(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        [HttpGet]
        [Route("GetInvoiceDetailsByInvoiceId/{id}")]
        public IActionResult GetInvoiceDetailsByInvoiceId(int id)
        {
            Invoice resData = _invoiceRepository.GetInvoiceDetailsByInvoiceId(id);

            if (resData.InvoiceId == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(resData);
            }
        }

        [HttpPost]
        [Route("GetInvoiceList")]
        public IActionResult GetInvoiceList(InvoiceFilter invoiceFilter)
        {
            List<Invoice> resData = _invoiceRepository.GetInvoiceList(invoiceFilter);

            if (resData.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(resData);
            }
        }

        [HttpPost]
        [Route("SaveInvoiceDetails")]
        public IActionResult SaveInvoiceDetails(Invoice invoice)
        {
            bool isSuccess = _invoiceRepository.SaveInvoice(invoice);
            if(isSuccess)
            {
                return Ok("Invoice Saved Successfully");
            }
            else
            {
                return Ok("Oops! somthing went wrong");
            }
        }

        [HttpPost]
        [Route("UpdateInvoiceDetails")]
        public IActionResult UpdateInvoiceDetails(Invoice invoice)
        {
            bool isSuccess = _invoiceRepository.UpdateInvoice(invoice);
            if (isSuccess)
            {
                return Ok("Invoice Updated Successfully");
            }
            else
            {
                return Ok("Oops! somthing went wrong");
            }
        }
    }
}
