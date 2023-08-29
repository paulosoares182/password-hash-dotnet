string password = "@Minha#S3Nh4!Sup3r$SEGurA";

ConsoleHelper.Init(password);

await Argon2Test.Test(password);
await Pbkdf2Test.Test(password, HashAlgorithmName.SHA1);
await Pbkdf2Test.Test(password, HashAlgorithmName.SHA256);
await Pbkdf2Test.Test(password, HashAlgorithmName.SHA512);
await Pbkdf2AspNetTest.Test(password, HashAlgorithmName.SHA1);
await Pbkdf2AspNetTest.Test(password, HashAlgorithmName.SHA256);
await Pbkdf2AspNetTest.Test(password, HashAlgorithmName.SHA512);
await BCryptTest.Test(password);