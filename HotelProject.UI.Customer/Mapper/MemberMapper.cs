using HotelProject.BL.Model;
using HotelProject.UI.CustomerWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.UI.CustomerWPF.Mapper
{
    public static class MemberMapper
    {
        public static MemberUI MapToUI(Member member)
        {
            return new MemberUI(member.Id, member.Name, member.BirthDay);

        }


        public static Member MapToDomain(MemberUI memberUI)
        {
            return new Member(memberUI.Id, memberUI.Name, memberUI.Birthday);
        }
    }

}
