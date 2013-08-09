DotNetNuke-Data-Expose
======================

Expose data from your DotNetNuke database utilizing the Web API framework.

My goal was to create a system wherein a website administrator could reuse content in other places in their own DotNetNuke website AND other websites which may or may not even be on the ASP.NET stack (I’m looking at you WordPress).

### How it can be used.
- Create a web service out of any data within your DotNetNuke Database.
- Add the feed to the XML/XSL module to clean up the data and show a list of users, articles, quotes, etc.
- Share content across multiple sites and third party websites. Web Service can be consumed by jQuery on the client or through server side handlers.
- Because it utilizes Web API data can transformed as <code>XML</code> or <code>JSON</code> based on Accept Header

### Installation
Install like any other module

### Requirements
Data Expose requires DotNetNuke 7.0 or higher and that’s it!

### Roadmap
The #1 feature on the road map is to bind service calls to the DotNetNuke business layer API and grant the ability to use SQL or direct API calls. Please give it a try and let me know what you think or if there are any additional features you would like to see added.


### License

<h4>New BSD License (BSD)</h4>
<p id="license_text">Copyright (c) 2012, Jonathan Sheely<br>All rights reserved.</p>
<p>Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:</p>
<p>* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.</p>
<p>* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.</p>
<p>* Neither the name of Jonathan Sheelynor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.</p>
<p>THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.</p>
