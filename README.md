# JWT
 JWT.Net

##Create Signature
```Csharp
            var password = Guid.NewGuid().ToString("N");

            var jwtp1 = new JWTPackage("yswenli", "jwt test", "everyone", DateTimeHelper.Now.AddMinutes(3).GetTimeStamp().ToString(),
                DateTimeHelper.Now.ToString(), DateTimeHelper.Now.ToString(), Guid.NewGuid().ToString("N"), password);

            var sign = jwtp1.Signature;
```
```Csharp
            var password = Guid.NewGuid().ToString("N");

            var jwtp1 = new JWTPackage<User>(new User()
            {
                Id = "1",
                Name = "yswenli",
                Role = "Admin"
            }, 180, password);

            var sign = jwtp1.Signature;
```
### Valide
```Csharp
            JWTPackage jwtp2 = null;

            try
            {
                jwtp2 = JWTPackage.Parse(sign, password);
            }
            catch (IllegalTokenException iex)
            {
                Console.WriteLine($"解析失败：{iex.Message}");
            }
            catch (TokenExpiredException tex)
            {
                Console.WriteLine($"解析失败：{tex.Message}");
            }
            catch (SignatureVerificationException sex)
            {
                Console.WriteLine($"解析失败：{sex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"解析失败：{ex.Message}");
            }

            if (jwtp2 != null)
                Console.WriteLine($"jwtp2.data:{jwtp2.Payload.Data}");
```