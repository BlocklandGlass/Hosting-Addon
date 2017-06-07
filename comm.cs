function GlassHosting::connectToServer(%instance) {
  %tcp = new TCPObject(GlassHostingTCP);
  %tcp.instance = %instance;
  %tcp.connect(%instance.address @ ":" @ %instance.port);

  %instance.tcp = %tcp;
  %instance.connecting = true;
}

function GlassHostingTCP::onConnected(%this) {
  echo("Connected to Glass Hosting " @ %this.instance.ident @ "!");
  %instance = %this.instance;
  %instance.connecting = false;
  %instance.connected  = true;
}

function GlassHostingTCP::onConnectFailed(%this) {
  echo("Connect failed!");
  cancel(%this.reconnect);

  %instance = %this.instance;
  %addr =  %instance.address @ ":" @ %instance.port;
  %this.reconnect = %this.schedule(1000, connect, %addr);
}

function GlassHostingTCP::onLine(%this, %line) {
  %call = getField(%line, 0);
  %json = getField(%line, 1);

  if(%call $= "usage")
    return;

  echo(%call);

  if(%call $= "console" || %call $= "chat")
    %json = strReplace(%json, "\\u", "[{u}]");

  if(%json !$= "") {
    if(jettisonParse(%json)) {
      echo("Glass Hosting parse failed");
      return;
    }

    %data = $JSON::Value;
  } else {
    %data = "";
  }

  switch$(%call) {
    case "serverStatus":
      echo("Status: " @ %data.status);
      %this.instance.status = %data.status;
      %this.instance.setStatus(%data.status);

    case "serverValue":
      for(%i = 0; %i < %data.keyCount; %i++) {
        %key = %data.keyName[%i];
        %val = %data.value[%key];

        %this.instance.value[%key] = %val;
        %this.instance.onValueUpdate(%key, %val);
      }

    case "console":
      for(%i = 0; %i < 10; %i++) {
        if(%i == 9)
          %char = "e";
        else
          %char = %i+1;

        %data.line = strReplace(%data.line, "[{u}]000" @ %char, collapseEscape("\\c" @ %i));
      }
      %this.instance.pushConsole(%data.line);

    case "chat":
      for(%i = 0; %i < 10; %i++) {
        if(%i == 9)
          %char = "e";
        else
          %char = %i+1;

        %data.text = strReplace(%data.text, "[{u}]000" @ %char, collapseEscape("\\c" @ %i));
      }
      %this.instance.pushChat(%data.text);
  }
}

function GlassHostingTCP::emit(%this, %call, %data) {
  if(%data !$= "") {
    if(!isObject(%data)) {
      error("%data must be a jettison object!");
      return;
    } else {
      %json = jettisonStringify("object", %data);
    }
  } else {
    %json = "";
  }

  %this.send(%call TAB %json @ "\r\n");
}
