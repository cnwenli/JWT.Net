# JWT
 JWT.Net

Create　
```Csharp
            var password = Guid.NewGuid().ToString("N");

            var jwtp1 = new JWTPackage("yswenli", "jwt test", "everyone", DateTimeHelper.Now.AddMinutes(3).GetTimeStamp().ToString(),
                DateTimeHelper.Now.ToString(), DateTimeHelper.Now.ToString(), Guid.NewGuid().ToString("N"), password);

            var sign = jwtp1.Signature;
```