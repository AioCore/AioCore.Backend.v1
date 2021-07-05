**Hướng dẫn tạo Plugin**
- Tạo project class library ở trong thư mục src\AioCore.Plugins
- Add Reference đến project AioCore.Application
- Chuột phải vào project vừa tạo Properties -> Build Events. 
Mục Post-build event command line, thêm vào nội dung sau:
*echo f | xcopy /f /y "$(TargetDir)$(TargetFileName)" "$(SolutionDir)src\AioCore.API\$(OutDir)AioCore.Plugins\$(TargetFileName)"*
