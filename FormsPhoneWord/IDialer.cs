﻿using System;using System.Threading.Tasks;namespace FormsPhoneWord {    public interface IDialer {        Task<bool> DialAsync(String phoneNumber);    }}