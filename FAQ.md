# This section contains frequently asked questions and official answers to them. Before creating an Issue, review this page.

*Question:* Why is this necessary?

__*Answer:* This repository has many goals. This is a code base used in the development of new software products, a tutorial for novice programmers, and, of course, the ability to quickly and effortlessly run a variety of neural algorithms directly on your home computer.__

*Question:* Can I use codes from this repository?

__*Answer:* This question should be asked to the authors of the original codes. C# implementations from this repository are distributed under the MIT license, which allows any use, but the presented codes are based on the codes of third-party developers (authors of algorithms), who have the right to install any license on their code. Thus, the priority is not our license, but the license of third - party programmers (you can find links to them in the README file of each algorithm).__

*Question:* Your implementations are very slow! Why? How do I speed them up?

__*Answer:* Indeed, C# implementations run significantly slower than the Python/Lua/C/C++ originals. This is due to the lack of hardware acceleration elements in the code repository (for example, support for SIMD or GPU computing) and some JIT compilation costs. You can speed up code execution either by changing their architecture and using a number of third-party libraries, or by providing more computing power.__

*Question:* Why C# and not C/C++?

__*Answer:* The C# language, unlike C/C++, is intuitive and secure. In addition, abandoning unmanaged code in favor of managed code allows you to make the program more hardware-independent.__

*Question:* Is my data protected?

__*Answer:* Complete user privacy is the basis of our policy. The organization's products never send or accept anything to the servers. No statistics, no user data, no anything else.__
