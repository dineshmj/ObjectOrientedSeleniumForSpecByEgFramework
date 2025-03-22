REM Look at the output from the Selenium Grid Hub Command Window. The IP address mentioned in the Hub output, and the IP address mentioned below must be the same.
REM If you update the JAR file, ensure that a copy is there in all three folders - Hub, Chrome Node, Firefox Node, and the command is modified accordingly.

java -Dwebdriver.gecko.driver="D:\Dropbox\Professional\GitHub\ObjectOrientedSeleniumForSpecByEgFramework\WebDrivers\Firefox\geckodriver.exe" -jar selenium-server-4.29.0.jar node --hub http://192.168.1.7:4444