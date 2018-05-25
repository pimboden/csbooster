var BrowserDetect = {
	init: function () {
		this.browser = this.searchString(this.dataBrowser) || "An unknown browser";
		this.version = this.searchVersion(navigator.userAgent)
			|| this.searchVersion(navigator.appVersion)
			|| "an unknown version";
		this.OS = this.searchString(this.dataOS) || "an unknown OS";
	},
	searchString: function (data) {
		for (var i=0;i<data.length;i++)	{
			var dataString = data[i].string;
			var dataProp = data[i].prop;
			this.versionSearchString = data[i].versionSearch || data[i].identity;
			if (dataString) {
				if (dataString.indexOf(data[i].subString) != -1)
					return data[i].identity;
			}
			else if (dataProp)
				return data[i].identity;
		}
	},
	searchVersion: function (dataString) {
		var index = dataString.indexOf(this.versionSearchString);
		if (index == -1) return;
		return parseFloat(dataString.substring(index+this.versionSearchString.length+1));
	},
	dataBrowser: [
		{ 	string: navigator.userAgent,
			subString: "OmniWeb",
			versionSearch: "OmniWeb/",
			identity: "OmniWeb"
		},
		{
			string: navigator.vendor,
			subString: "Apple",
			identity: "Safari"
		},
		{
			prop: window.opera,
			identity: "Opera"
		},
		{
			string: navigator.vendor,
			subString: "iCab",
			identity: "iCab"
		},
		{
			string: navigator.vendor,
			subString: "KDE",
			identity: "Konqueror"
		},
		{
			string: navigator.userAgent,
			subString: "Firefox",
			identity: "Firefox"
		},
		{
			string: navigator.vendor,
			subString: "Camino",
			identity: "Camino"
		},
		{		// for newer Netscapes (6+)
			string: navigator.userAgent,
			subString: "Netscape",
			identity: "Netscape"
		},
		{
			string: navigator.userAgent,
			subString: "MSIE",
			identity: "Explorer",
			versionSearch: "MSIE"
		},
		{
			string: navigator.userAgent,
			subString: "Gecko",
			identity: "Mozilla",
			versionSearch: "rv"
		},
		{ 		// for older Netscapes (4-)
			string: navigator.userAgent,
			subString: "Mozilla",
			identity: "Netscape",
			versionSearch: "Mozilla"
		}
	],
	dataOS : [
		{
			string: navigator.platform,
			subString: "Win",
			identity: "Windows"
		},
		{
			string: navigator.platform,
			subString: "Mac",
			identity: "Mac"
		},
		{
			string: navigator.platform,
			subString: "Linux",
			identity: "Linux"
		}
	]

};

function getScreenCenterY() 
{   
   var y = 0;   
   y = getScrollOffset()+(getInnerHeight()/2);   
   return(y);   
}   
  
function getScreenCenterX() 
{   
   return(document.body.clientWidth/2);   
}   
  
function getInnerHeight() 
{   
   var y;   
   if (self.innerHeight) // all except Explorer   
   {   
   y = self.innerHeight;   
   }   
   else if (document.documentElement && document.documentElement.clientHeight)   
   {   
      // Explorer 6 Strict Mode   
      y = document.documentElement.clientHeight;   
   }   
   else if (document.body) // other Explorers   
   {   
      y = document.body.clientHeight;   
   }   
   return(y);   
}   
  
function getScrollOffset() 
{   
   var y;   
   if (self.pageYOffset) // all except Explorer   
   {   
      y = self.pageYOffset;   
   }   
   else if (document.documentElement && document.documentElement.scrollTop)   
   // Explorer 6 Strict   
   {   
      y = document.documentElement.scrollTop;   
   }   
   else if (document.body) // all other Explorers   
   {   
      y = document.body.scrollTop;   
   }   
   return(y);   
}  




