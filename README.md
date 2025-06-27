# Comment Cleaner

This is an application developed to delete Naver comments all at once.
The initial purpose was to delete comments in bulk, but during development, I added an auto-spam feature as it seemed feasible.
The development language is C#, using the WPF framework, and the bvsd generation part is implemented in JavaScript using the JInt library.

It analyzes and bypasses the security used by Naver during login,
and when writing comments, it analyzes and implements the bvsd defined by Naver exactly as it is.

## bvsd
Naver requires a bvsd defined by Naver when logging in. (It didn't exist in 2017, but it seems to have been created in 2018)
This must be analyzed and implemented exactly the same way to pass it for successful login.
Actually, I implemented it once in 2017, but when I tried again in 2018, it didn't work, so I found this while looking for the reason. It seems Naver is also paying attention to this.

## Packet Analysis
After successful login, you just need to capture packets for comment registration, modification, and deletion properly and send the same packets.
Since it's transmitted via https, packets aren't easy to capture, but you can find the transmission URL in the JavaScript used by Naver. By changing https to http and re-emulating the js, you can capture and view the data.

## Development Documentation
* [Naver Login Security Analysis - 1 (Session Key)](https://blog.naver.com/boyism/221424546234)
* [Naver Login Security Analysis - 2 (bvsd)](https://blog.naver.com/boyism/221424548358)
* [Naver Login Security Analysis - 3 (bvsd:uuid)](https://blog.naver.com/boyism/221424549546)
* [Naver Login Security Analysis - 4 (bvsd:keyboard)](https://blog.naver.com/boyism/221424550656)
* [Naver Login Security Analysis - 5 (bvsd:device)](https://blog.naver.com/boyism/221424551365)
* [Naver Login Security Analysis - 6 (bvsd:mouse)](https://blog.naver.com/boyism/221424552716)
* [Naver Login Security Analysis - 7 (bvsd:duration, hash, components)](https://blog.naver.com/boyism/221424553997)
* [Naver Login Security Analysis - 8 (bvsd encoding)](https://blog.naver.com/boyism/221424555109)
* [Naver Login Security Analysis - 9 (transmission, conclusion)](https://blog.naver.com/boyism/221424556615)
* [Naver News Comment Auto-posting/Deletion](https://blog.naver.com/boyism/221430121772)

## Demo
* [Watch YouTube Video](https://youtu.be/As5ZQz000tg)
