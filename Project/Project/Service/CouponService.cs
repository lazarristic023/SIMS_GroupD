using Project.Model;
using Project.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service
{
    public class CouponService
    {
        CouponRepository couponRepository;

        public CouponService()
        {
            couponRepository = new CouponRepository();
        }


        public int Create(int guestId, DateTime dateOfExpire)
        {
            Coupon coupon = new Coupon(guestId,dateOfExpire);

            int couponId = couponRepository.Add(coupon);

            return couponId;

        }

        public Coupon GetById(int id)
        {
            return couponRepository.GetById(id);
        }

        public List<Coupon> GetAll()
        {
            return couponRepository.GetAll();
        }

        public void Remove(int id)
        {
            couponRepository.Remove(id);
        }

        

    }
}
