<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmailTemplate.ascx.cs" Inherits="Modules_HR_EmailTemplate" %>
<%@ Import Namespace="System.ComponentModel"  %>
<script runat="server">
private ITemplate messageTemplate = null;

[ TemplateContainer(typeof(MessageContainer)) ]
[ PersistenceMode(PersistenceMode.InnerProperty) ]
public ITemplate MessageTemplate {
    get 
    { 
        return messageTemplate; 
    }
    set 
    { 
        messageTemplate = value; 
    }
}

void Page_Init() {
    if (messageTemplate != null) {
        String[] fruits = {"apple", "orange", "banana", "pineapple" };
        for (int i=0; i<4; i++) 
        {
            MessageContainer container = new MessageContainer(i, fruits[i]);
            messageTemplate.InstantiateIn(container);
            PlaceHolder1.Controls.Add(container);
        }
    }
}

public class MessageContainer: Control, INamingContainer {
    private int m_index;
    private String m_message;
    internal MessageContainer(int index, String message)
    { 
        m_index = index;
        m_message = message;
    }
    public int Index {
        get 
        { 
            return m_index; 
        } 
    }
    public String Message 
    { 
        get 
        { 
            return m_message; 
        } 
    }
}
</script>
<asp:Placeholder runat="server" ID="PlaceHolder1" />