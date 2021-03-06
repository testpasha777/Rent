﻿using BLL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModel;
using DAL.Repositories;
using DAL.Interface;
using DAL.Entities.Entities;

namespace BLL.Services
{
    public class AvailableToGuestService : IAvailableToGuestService
    {
        private IAvailableToGuestRepository availableToGuestRep;

        public AvailableToGuestService(IAvailableToGuestRepository _availableToGuestRep)
        {
            availableToGuestRep = _availableToGuestRep;
        }

        public bool Create(AvailableToGuestCreateViewModel availableToGuestVM)
        {
            var availableToGuest = availableToGuestRep.GetByName(availableToGuestVM.Name);

            if(availableToGuest != null)
            {
                return false;
            }

            AvailableToGuest add = new AvailableToGuest();
            add.Name = availableToGuestVM.Name;
            availableToGuestRep.Create(add);
            availableToGuestRep.SaveChanges();
            return true;
        }

        public void Delete(int id)
        {
            availableToGuestRep.Delete(id);
            availableToGuestRep.SaveChanges();
        }

        public AvailableToGuestViewModel GetById(int id)
        {
            var availableToGuest = availableToGuestRep.GetById(id);

            return new AvailableToGuestViewModel
            {
                Id = availableToGuest.Id,
                Name = availableToGuest.Name
            };
        }

        public AvailableToGuestEditViewModel GetEditById(int id)
        {
            var availableToGuest = availableToGuestRep.GetById(id);

            return new AvailableToGuestEditViewModel
            {
                Id = availableToGuest.Id,
                Name = availableToGuest.Name
            };
        }

        public IEnumerable<AvailableToGuestViewModel> GetAll()
        {
            var availableToGuests = availableToGuestRep.GetAll().Select(i => new AvailableToGuestViewModel
            {
                Id = i.Id,
                Name = i.Name
            });

            return availableToGuests;
        }

        public bool Update(AvailableToGuestEditViewModel availableToGuestEditVM)
        {
            var availableToGuest = availableToGuestRep.GetByName(availableToGuestEditVM.Name);

            if(availableToGuest != null && availableToGuest.Id != availableToGuestEditVM.Id)
            {
                return false;
            }

            var update = availableToGuestRep.GetById(availableToGuestEditVM.Id);
            update.Name = availableToGuestEditVM.Name;
            availableToGuestRep.SaveChanges();
            return true;
        }
    }
}
