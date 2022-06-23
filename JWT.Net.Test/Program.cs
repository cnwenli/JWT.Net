using JWT.Net.Common;
using JWT.Net.Exceptions;
using System;
using System.Diagnostics;

namespace JWT.Net.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter to start the test");
            Console.ReadLine();

            Test1();

            Test2();

            Test3();

            Console.WriteLine("Enter to end the test");
            Console.ReadLine();
        }

        static void Test1()
        {
            var password = Guid.NewGuid().ToString("N");

            var jwtp1 = new JWTPackage("yswenli", "jwt test", "everyone", DateTimeHelper.Now.AddMinutes(3).GetTimeStamp().ToString(),
                DateTimeHelper.Now.ToString(), DateTimeHelper.Now.ToString(), Guid.NewGuid().ToString("N"), password);

            var sign = jwtp1.GetBearerToken();

            Console.WriteLine($"jwt.signature:\r\n{sign}");

            JWTPackage jwtp2 = null;

            try
            {
                jwtp2 = JWTPackage.Parse(sign, password);
            }
            catch (IllegalTokenException iex)
            {
                Console.WriteLine($"Parsing failed：{iex.Message}");
            }
            catch (TokenExpiredException tex)
            {
                Console.WriteLine($"Parsing failed：{tex.Message}");
            }
            catch (SignatureVerificationException sex)
            {
                Console.WriteLine($"Parsing failed：{sex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Parsing failed：{ex.Message}");
            }

            if (jwtp2 != null)
                Console.WriteLine($"jwtp2.data:{jwtp2.Payload.Data}");
            Console.WriteLine();
        }

        static void Test2()
        {
            var password = Guid.NewGuid().ToString("N");

            var jwtp1 = new JWTPackage<User>(new User()
            {
                Id = "1",
                Name = "yswenli",
                Role = "Admin"
            }, 3, password);

            var sign = jwtp1.GetBearerToken();

            Console.WriteLine($"jwt.signature:\r\n{sign}");

            JWTPackage<User> jwtp2 = null;

            try
            {
                jwtp2 = JWTPackage<User>.Parse(sign, password);
            }
            catch (IllegalTokenException iex)
            {
                Console.WriteLine($"Parsing failed：{iex.Message}");
            }
            catch (TokenExpiredException tex)
            {
                Console.WriteLine($"Parsing failed：{tex.Message}");
            }
            catch (SignatureVerificationException sex)
            {
                Console.WriteLine($"Parsing failed：{sex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Parsing failed：{ex.Message}");
            }

            if (jwtp2 != null)
                Console.WriteLine($"jwtp2.data:{jwtp2.Payload.Data}");

            Console.WriteLine();
        }

        static void Test3()
        {
            Console.Write("Performance testing in progress");

            Stopwatch stopwatch = Stopwatch.StartNew();

            var count = 1000000;

            var password = Guid.NewGuid().ToString("N");

            for (int i = 0; i < count; i++)
            {
                var jwt1 = new JWTPackage<User>(new User()
                {
                    Id = "1",
                    Name = "yswenli",
                    Role = "Admin"
                }, 180, password);

                var sign = jwt1.GetBearerToken();

                JWTPackage<User>.Parse(sign, password);
            }
            stopwatch.Stop();

            Console.WriteLine($"\rAt the end of the performance test, the speed is：{count / stopwatch.Elapsed.TotalSeconds} times/s");
            Console.WriteLine();
        }
    }
}
