
	var Key;
	var PreviousKey;
	var SubKey;
    var setUrl;
    var currentUrl;
    var MainMenuCanvasLeft;
    var PrivilegesList;
    
Scene = function() 
{     
}

Scene.prototype =
{
	handleLoad: function(plugIn, userContext, rootElement) 
	{
//		this.plugIn = plugIn;
//		
//		// Sample button event hookup: Find the button and then attach event handlers
//		this.button = rootElement.children.getItem(0);	
//		
//		this.button.addEventListener("MouseEnter", Silverlight.createDelegate(this, this.handleMouseEnter));
//		this.button.addEventListener("MouseLeftButtonDown", Silverlight.createDelegate(this, this.handleMouseDown));
//		this.button.addEventListener("MouseLeftButtonUp", Silverlight.createDelegate(this, this.handleMouseUp));
//		this.button.addEventListener("MouseLeave", Silverlight.createDelegate(this, this.handleMouseLeave));

    currentUrl= location.href;
    currentUrl=currentUrl.split("?");
    currentUrl=currentUrl[0].split("/");
    currentUrl=currentUrl[currentUrl.length-2]+"/"+currentUrl[currentUrl.length-1];


    HiddenFieldP();

    LoadMainMenuHeads(rootElement);
    MasterMenuLoad(rootElement);
    SMMenuLoad(rootElement);
     InventoryMenuLoad(rootElement);
    SCMMenuLoad(rootElement);
   
    SupportMenuLoad(rootElement);
    FinanceMenuLoad(rootElement);
    HRMenuLoad(rootElement);
    ReportsMenuLoad(rootElement);
    
    
  
   
	},
	
	// Sample event handlers
	handleMouseEnter: function(sender, eventArgs) 
	{
		// The following code shows how to find an element by name and call a method on it.
		var mouseEnterAnimation = sender.findName("mouseEnter");
		mouseEnterAnimation.begin(); 
	},
	
	handleMouseDown: function(sender, eventArgs) 
	{
		var mouseDownAnimation = sender.findName("mouseDown");
		mouseDownAnimation.begin(); 
	},
	
	handleMouseUp: function(sender, eventArgs) 
	{
		var mouseUpAnimation = sender.findName("mouseUp");
		mouseUpAnimation.begin(); 
		
		// Put clicked logic here
		alert("clicked");
	},
	
	handleMouseLeave: function(sender, eventArgs) 
	{
		var mouseLeaveAnimation = sender.findName("mouseLeave");
		mouseLeaveAnimation.begin(); 
	}
}



function MouseEnter(sender, eventArgs)
{
    var label = sender.name;
    
    MainMenuSelect(sender,label);
    sender.findName(label+"_Rectangle").Opacity="0";
    
    sender.findName("OverLight_Rect").width="0";
    sender.findName("SelectLight_Rect").width="0";
    sender.findName("SelectLight_Arrow").Opacity="0";
    if(Key!=undefined)
    {
        if(label==Key)
        {
            sender.findName(Key+"_Menu_Canvas")["Canvas.Top"]="20";
            sender.findName(Key+"_Menu_Canvas").Opacity=1;  
        }
        else
        {        
            sender.findName(Key+"_Menu_Canvas")["Canvas.Top"]="140";
            sender.findName(Key+"_Menu_Canvas").Opacity=0;  
        }
    }
    if(SubKey!=undefined)
    {      
        if(label==SubKey.split("_",1))
        {
            SubMenuItemsSelect(sender,SubKey);
        }
    }
}

function MouseLeftClick(sender, eventArgs)
{
    var label = sender.name;
    PreviousKey=Key;
    Key=label;
    
    DeSelect(sender);
    
    MainMenuSelect(sender,label);
    if(SubKey!=undefined)
    {
        if(label==SubKey.split("_",1))
        {
            SubMenuItemsSelect(sender,SubKey);
        }  
    }
    sender.findName("MainMenu_OverLight").width="0";
}

function MouseLeave (sender, eventArgs)
{
    sender.findName("MainMenu_OverLight").width="0";

    var label = sender.name;
    if(Key==label)
    {
        MainMenuSelect(sender,label);
    }
    else
    {
        MainMenuDeselect(sender,label);
        SubMenuItemsDeSelect(sender,SubKey);
        if(Key!=undefined)
        {
            MainMenuSelect(sender,Key);
            if(SubKey!=undefined)
            {
                if(Key==SubKey.split("_",1))
                {
                    SubMenuItemsSelect(sender,SubKey);
                } 
            } 
        }  
    }
}

function ItemsMouseEnter(sender, eventArgs)
{
    var label = sender.name;
    SubMenuItemsHOver(sender,label)
}

function ItemsMouseLeftClick(sender, eventArgs)
{
    var label = sender.name;
//    var ValidOrNot;
    if(hfp!="")
    {
       PrivilegesList=hfp.split("#|#");
        
        for(var i=0;i<=PrivilegesList.length;i++)
        {
            if(PrivilegesList[i]==label)
            {
                ValidOrNot=true;
                break;
            }
            else
            {
                ValidOrNot=false;
            } 
        }
        
        if(ValidOrNot==false)
        {
            MainMenuDeselect(sender,Key);
            if(PreviousKey!=undefined)
            {
                Key=PreviousKey;
                MainMenuSelect(sender,PreviousKey);
                if(SubKey!=undefined)
                {
                    SubMenuItemsSelect(sender,SubKey);
                }
            }
            else
            {
                MainMenuSelect(sender,Key);
            }
            alert("Sorry ......... ! You Are Not Having Privilege To Access This Page. Please Contact Administrator");
            return;
        }
    }
    
    if(SubKey!=undefined)
    {
        sender.findName(SubKey).Foreground="White";   
    } 
    SubKey=label;
    SubMenuItemsSelect(sender,label)
    //////////////////////////////////////////////////////////////////////
    NavigateToUrl(label)
}

function ItemsMouseLeave(sender, eventArgs)
{
    var label = sender.name;
    if(SubKey==label)
    {
        SubMenuItemsHOverRemove(sender,label)
        sender.findName(label).Foreground="Black";
    }
    else
    {
        SubMenuItemsHOverRemove(sender,label)
    }
}


function MainMenuSelect(sender,MainMenuName)
{
    sender.findName(MainMenuName).Foreground="Black"; 
    sender.findName("MainMenu_OverLight_Canvas")["Canvas.Left"]=sender.findName(MainMenuName+"_Canvas")["Canvas.Left"];
    sender.findName("MainMenu_OverLight").width=sender.findName(MainMenuName).Actualwidth+10;       
    sender.findName(MainMenuName+"_Rectangle").Opacity=1;
    sender.findName(MainMenuName+"_Rectangle").width=sender.findName(MainMenuName).Actualwidth+10;   
    sender.findName(MainMenuName+"_Menu_Canvas")["Canvas.Top"]="20";
    sender.findName(MainMenuName+"_Menu_Canvas").Opacity=1;
}

function MainMenuDeselect(sender,MainMenuName)
{
    sender.findName(MainMenuName).Foreground="White";    
    sender.findName(MainMenuName+"_Rectangle").width="0";
    sender.findName(MainMenuName+"_Menu_Canvas")["Canvas.Top"]="140";
    sender.findName(MainMenuName+"_Menu_Canvas").Opacity=0;
}

function SubMenuItemsSelect(sender,SubMenuItemName)
{
    sender.findName(SubMenuItemName).Foreground="Black";
    sender.findName("SelectLight_Rect").width=sender.findName(SubMenuItemName).Actualwidth+30;
    sender.findName("SelectLight")["Canvas.Left"]=sender.findName(SubMenuItemName)["Canvas.Left"]-18;
    sender.findName("SelectLight")["Canvas.Top"]=sender.findName(SubMenuItemName)["Canvas.Top"]+20;
    sender.findName("SelectLight_Arrow").Opacity=1;
    sender.findName("OverLight_Rect").width="0";
}

function SubMenuItemsDeSelect(sender,SubMenuItemName)
{
    sender.findName("SelectLight_Rect").width="0";
    sender.findName("OverLight_Rect").width="0";
    sender.findName("SelectLight_Arrow").Opacity=0;
    sender.findName("OverLight_Arrow").Opacity=0;
}


function SubMenuItemsHOver(sender,SubMenuItemName)
{
    sender.findName(SubMenuItemName).Foreground="Black";
    sender.findName("OverLight_Rect").width=sender.findName(SubMenuItemName).Actualwidth+30;
    sender.findName("OverLight_Arrow").Opacity=1;
    sender.findName("OverLight")["Canvas.Left"]=sender.findName(SubMenuItemName)["Canvas.Left"]-18;
    sender.findName("OverLight")["Canvas.Top"]=sender.findName(SubMenuItemName)["Canvas.Top"]+20;
}

function SubMenuItemsHOverRemove(sender,SubMenuItemName)
{
    sender.findName(SubMenuItemName).Foreground="White";    
    sender.findName("OverLight_Rect").width="0";
    sender.findName("OverLight_Arrow").Opacity=0;
}