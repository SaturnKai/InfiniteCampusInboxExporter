Infinite Campus Inbox Exporter is a program I wrote in C# that allows you to export your Infinite Campus inbox into a backup folder.
This tool can be used for archiving your inbox as a backup.

Download Instructions:
-----

 - Download and install .NET Core V3.1 Runtime
   - Windows 64 Bit: https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.0-windows-x64-installer
   - Windows 32 Bit: https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-desktop-3.1.0-windows-x86-installer
 - Download the Release at: https://github.com/SaturnKai/InfiniteCampusInboxExporter/releases/download/1.0/InfiniteCampusInboxExporter-V1.0.zip
 - Extract the .ZIP file
 - Run the "InfiniteCampusInboxExporter" Executable

Program Instructions:
-----

When executing the program, you will be prompted to enter in your state code and your school district's name. These are required to pinpoint the correct Infinite Campus server that corresponds to the correct school district you go to. The same information that you would enter on https://www.infinitecampus.com/audience/parents-students/login-search is the same information you would enter here, as it's required to authenticate your account correctly. After entering your district information, you will be greeted with a result, or multiple results if any valid districts were found. If none were found, this is either because you entered something incorrectly, or your school district is labeled under a different name. If any results appear, then enter in the number that corresponds to the correct result.

After that, you will be prompted to enter in your Infinite Campus username and password. This is required as Infinite Campus will only allow access to your inbox if your account is authenticated. If the login doesn’t work correctly and returns an error, this is either an issue with Infinite Campus’s login being down, or your username or password wasn’t entered correctly.

After that, your Infinite Campus inbox will be accessed via the Infinite Campus API, and every inbox entry will be downloaded into a folder of the message’s date, in the format of a .html file.

**Developer’s Note:** I’m aware this program isn’t super useful, this project was created more for fun, however some may be able to get some use from it. Maybe someday I can branch this project into something bigger, like a fully custom Infinite Campus client. I put a lot of effort into this program, so I hope you enjoy it as much as I enjoyed making it! :)
