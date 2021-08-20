using System;
using Xunit;

namespace SampleTest
{
    public class FinalTest
    {
        [Fact]
        public void ValidUserName()
        {
            const string name = "";
            Boolean expectedResult = true;
            Boolean actualResult = Validation.IsNameNull(name);
            Assert.Equal(expectedResult,actualResult);
        }

        [Fact]
        public void ValidEmail()
        {
            const string mail = "hromila05@gmail.com";
            Boolean actualResult = Validation.IsValidAddress(mail);
            Assert.True(actualResult,$"The email {mail} is not valid");
        }

        [Fact]
        public void ValidPassword()
        {
            const string pass = "Romila@05";
            Boolean actualResult = Validation.IsValidPassword(pass);
            Assert.True(actualResult,$"The password {pass} is not valid");
        }

        [Fact]
        public void NotValidEmail()
        {
            const string mail = "abc123";
            Boolean actualResult = Validation.IsValidAddress(mail);
            Assert.False(actualResult,$"The password {mail} should not be valid");
        }

        [Fact]
        public void IsPassworNullOrEmpty()
        {
            const string pass = "";
            Boolean actualResult = Validation.IsValidPassword(pass);
            Assert.False(actualResult,$"The password {pass} should not be valid");
        }

        [Theory]
        [InlineData("Th1sIsp@ssword",true)]
        [InlineData("Abc$123456",true)]
        [InlineData("thisIsAPassword",false)]
        [InlineData("thisispassword#",false)]
        [InlineData("",false)]
        public void ValidatePassword(string password, Boolean expectedResult)
        {
            bool isValid = Validation.IsValidPassword(password);

            Assert.Equal(expectedResult, isValid);
        }


        
    }
}
