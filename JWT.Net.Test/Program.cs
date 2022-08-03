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

            Test4();

            Console.WriteLine("Enter to end the test");
            Console.ReadLine();
        }

        static void Test1()
        {
            var password = Guid.NewGuid().ToString("N");

            var jwtp1 = new JWTPackage("yswenli", "jwt test", "everyone", DateTimeHelper.Now.AddMinutes(3).GetTimeStamp(),
                DateTimeHelper.Now.GetTimeStamp(), DateTimeHelper.Now.GetTimeStamp(), Guid.NewGuid().ToString("N"), password);

            var sign = jwtp1.GetToken();

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

            var sign = jwtp1.GetToken();

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
            //var jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJ5c3dlbmxpLmNuYmxvZ3MuY29tIiwiVXNlcklkIjoiMSIsIlVzZXJOYW1lIjoiYWRtaW4iLCJOYW1lIjoiYWRtaW5pc3RyYXRvciIsIk1vYmlsZSI6IjE1ODIxNDM5OTM0IiwiR3JvdXBJRCI6IjEiLCJleHAiOjE2OTEwMjY0MDcsImF1ZCI6IldlYkFwaSJ9.m5tX_LQFFlV2Q0QwXjyk8312jBQ1OheP6TV-6AjYBdE";
            var jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJ5c3dlbmxpLmNuYmxvZ3MuY29tIiwiVXNlcklkIjoiMSIsIlVzZXJOYW1lIjoiYWRtaW4iLCJOYW1lIjoiYWRtaW5pc3RyYXRvciIsIk1vYmlsZSI6IjE1ODIxNDM5OTM0IiwiR3JvdXBJRCI6IjEiLCJleHAiOjE2OTEwNDMxNDYsImF1ZCI6IldlYkFwaSJ9.k0Uo9RigQ7QQRDp96hX34RB6sgAbUWY-1QIGfUhi2Mo";
            //var jp = JWTPackage.Parse(jwt, "base64:HU8MlQQDHfGaQ+k+0q3z4HKJvNQUTjK5uRGodDATyKc=");
            var jp = JWTPackage<AuthUserInfo>.Parse(jwt, "base64:HU8MlQQDHfGaQ+k+0q3z4HKJvNQUTjK5uRGodDATyKc=");
            Console.WriteLine(jp.Payload.Data);
        }

        static void Test4()
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

                var sign = jwt1.GetToken();

                JWTPackage<User>.Parse(sign, password);
            }
            stopwatch.Stop();

            Console.WriteLine($"\rAt the end of the performance test, the speed is：{count / stopwatch.Elapsed.TotalSeconds} times/s");
            Console.WriteLine();
        }


    }


    //
    // 摘要:
    //     用户信息
    public class AuthUserInfo
    {
        //
        // 摘要:
        //     用户id
        public string UserID { get; set; }

        //
        // 摘要:
        //     用户名
        public string UserName { get; set; }

        //
        // 摘要:
        //     姓名
        public string Name { get; set; }

        //
        // 摘要:
        //     手机
        public string Mobile { get; set; }

        //
        // 摘要:
        //     角色组id
        public string GroupID { get; set; }
    }
}
