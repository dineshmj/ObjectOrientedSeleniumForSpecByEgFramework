You need the latest version of Java Development Kit (JDK) to run Selenium Grid.
You can find the 64-bit Windows version here:

	https://www.oracle.com/pk/java/technologies/downloads/
	
		>> Click on "Windows" tab.
		>> Click on the download link against the text "x64 Installer".
		>> Run the installer.

Go to the "Hub" folder, invoke a CMD window, and run the "StartServer.cmd" file.
It invokes the following command:

	java -jar selenium-server-<<version_number>>.jar hub

When the Hub runs, the CMD output will show a registration URL (an IP address with a port-number, typically 4444).
Please note the IP address and Port number in the URL.
	
		a) Ensure that you have updated the Registration Hub URL in the "appSettings.JSON" file in your Web UI Tests project, with the URL that you see in the CMD output window.
		b) Ensure that the IP address and the port number that you see in the URL, are exactly the same in the "Chrome Node" and "Firefox Node" CMD files (see in respective folders).
