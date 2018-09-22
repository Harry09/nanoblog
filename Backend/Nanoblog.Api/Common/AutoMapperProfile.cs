using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Nanoblog.Api.Data.Dto;
using Nanoblog.Api.Data.Models;

namespace Nanoblog.Api.Common
{
	class AutoMapperProfile
	{
		public AutoMapperProfile()
		{
			Mapper.Initialize(config =>
			{
				config.CreateMap<User, UserDto>();
				config.CreateMap<Entry, EntryDto>();
			});
		}
	}
}
