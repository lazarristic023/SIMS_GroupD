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
        public Guest2 Guest { get; set; }

        public CouponService()
        {
            couponRepository = new CouponRepository();
            Guest = new Guest2();
            LinkGuest2Coupons();
        }

        public CouponService(User u)
        {
            couponRepository = new CouponRepository();
            Guest = new Guest2(u);
            LinkGuest2Coupons();
        }

        private void LinkGuest2Coupons()
        {
            foreach(var coupon in couponRepository.GetAll())
            {
                if((coupon.GuestId == Guest.User.Id) && (coupon.Status == Coupon.STATUS.NOTUSED))
                {
                    Guest.Coupons.Add(coupon);
                }
            }
        }

        public List<Coupon> GetGuest2Coupons()
        {
            return Guest.Coupons;
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

        public void ChangeCouponToUsed(int id)
        {
            couponRepository.ChangeToUsed(id);
        }

    }
}
