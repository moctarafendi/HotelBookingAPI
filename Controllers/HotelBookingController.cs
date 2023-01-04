using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Data;
using HotelBookingAPI.Models;

namespace HotelBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelBookingController : ControllerBase
    {
        private readonly ApiContext _context;
        public HotelBookingController(ApiContext context)
        {
            _context = context;
        }

        // Create/ Edit
        [HttpPost]
        public JsonResult CreateEdit(HotelBooking booking)
        {
            //cek apakah id yang diinput 0 atau tidak
            if (booking.Id == 0)
            {
                //jika iya maka akan melakukan create data atau add data
                _context.Bookings.Add(booking);
            }
            else
            {
                //jika bukan 0 maka akan mencari id tersebut
                var bookingIdInDb = _context.Bookings.Find(booking.Id);
                //jika id tidak ditemukan maka akan return not found
                if (bookingIdInDb == null)
                    return new JsonResult(NotFound());
                //jika ada maka akan merubah data dengan data yang diinput tadi
                bookingIdInDb = booking;
            }
            _context.SaveChanges();
            //kemudian return status code ok dan data dalam bentuk JSON
            return new JsonResult(Ok(booking));
        }

        // Get
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            //cek apakah id ada didalam database atau tidak
            var result = _context.Bookings.Find(id);
            //jika tidak ada maka akan return not found status code
            if (result == null)
                return new JsonResult(NotFound());
            // jika ada maka akan return status code ok dan data dalam bentuk JSON
            return new JsonResult(Ok(result));
        }

        // Delete
        [HttpDelete()]
        public JsonResult Delete(int id)
        {
            //cek apakah id ada di dalam db atau tidak
            var result = _context.Bookings.Find(id);
            //jika tidak ada maka akan return not found status code
            if (result == null)
                return new JsonResult(NotFound());
            //jika ada maka akan dihapus
            _context.Bookings.Remove(result);
            _context.SaveChanges();
            //kemudian return NoContent status code untuk menandakan bahwa data sudah dihapus
            return new JsonResult(NoContent());
        }

        // Get All
        [HttpGet]
        public JsonResult GetAll()
        {
            //ambil semua data dari database dan list
            var result = _context.Bookings.ToList();

            //kemudian return status code ok dan data dalam bentuk JSON
            return new JsonResult(Ok(result));
        }

    }
}
