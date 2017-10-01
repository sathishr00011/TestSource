<?xml version="1.0" encoding="iso-8859-1" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
  <xsl:template match="/">

    <html>
      <head>
        <SCRIPT language="JavaScript">
          <![CDATA[

var time;
function stopClock()
{
clearTimeout(time);
}
function yourClock()
{

var nd = new Date();
var h, m, s,h1,m1;
var time="";
h = nd.getHours();
m = nd.getMinutes();
s = nd.getSeconds();
if (s <=9) 
s="0" + s;
if (m <=9)
m="0" + m;
if (h <=9) 
h="0" + h;
if (h >12)
h=h-12;
h1=h-5;
m1=m-30;
if (m1 < 0)
{
m1=60+m1;
h1=h1-1;
}
if (h1 < 0)
h1=12+h1;
if (m1 <=9)
m1="0" + m1;
if (h1 <=9) 
h1="0" + h1;
time+=" India: "+h+":"+m+":"+s+" -- UK:"+h1+":"+m1+":"+s;

the_time.value=time;
time=setTimeout("yourClock()",1000);
}

]]>
        </SCRIPT>

      </head>
      <body bgcolor="#F5DEB3" onload="yourClock();" onunload="stopClock(); return true">
        <center>
          <h4>
            <FONT FACE="VERDANA, Century Schoolbook">

              REWARDS DEPLOYMENT DETAILS


            </FONT>
          </h4>

          <table height="2%" cellspacing="1" width="75%">
            <tbody>
              <tr id="datetime" style="display: block" valign="center">
                <td align="middle">
                  <b>
                    <script language="JavaScript">
                      <![CDATA[
var mydate=new Date()
var year=mydate.getYear()
var day=mydate.getDay()
var month=mydate.getMonth()
var daym=mydate.getDate()
if (daym<8)
daym="0"+daym
var dayarray=new Array("Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday")
var montharray=new Array("January","February","March","April","May","June","July","August","September","October","November","December")
document.write("<small><font color='000000' face='Arial'><b>"+dayarray[day]+", "+montharray[month]+" "+daym+","+year+"</b></font></small>")
]]>
                    </script>
                    <br></br>
                    <br></br>
                    <input size="25" name="the_time"></input>
                  </b>
                </td>
              </tr>
            </tbody>
          </table>
        </center>

        <left>
          <table>
            <tbody>
              <tr>
                <td>
                  <b>
                    SPRINT STARTE DATE: <xsl:value-of select="concat(substring(REWARDSS/WEBSITE/SPrintStart,1,4),'-',substring(REWARDSS/WEBSITE/SPrintStart,5,2),'-',substring(REWARDSS/WEBSITE/SPrintStart,7,2))"/>
                  </b>
                </td>
              </tr>

            </tbody>
          </table>
        </left>
        <table height="1%" cellspacing="0" width="100%" border="0">
          <tbody>
            <tr>
              <td colspan="2">
              </td>
            </tr>
            <tr id="main" style="display: block" valign="center">
              <td align="left" width="100%">
                <table height="1%" cellspacing="0" width="100%" border="2">
                  <tbody>
                    <tr id="APPLICATION" style="display: block;background-color:3C3B61;color:FFFFF1" valign="middle">
                      <th align="center">
                        <b>
                          COMPONENTTYPE
                        </b>
                      </th>
                      <th align="center">
                        <b>
                          COMPONENTNAME
                        </b>
                      </th>
                      <th align="center">
                        <b>
                          DEV - MANIFEST
                        </b>
                      </th>
                      <th align="center">
                        <b>
                          PPE - MANIFEST
                        </b>
                      </th>
                      <th align="center">
                        <b>
                          LIVE - MANIFEST
                        </b>
                      </th>
                    </tr>
                    <xsl:for-each select="REWARDSS/WEBSITE">
                      <tr>
                        <td align="center">
                          <b>
                            <xsl:value-of select="COMPONENTTYPE"/>

                          </b>
                        </td>



                        <td align="left">
                          <b>
                            <xsl:value-of select="COMPONENTNAME"/>

                          </b>
                        </td>


                        <xsl:choose>
                          <!--<xsl:when test ="CE_DEVNEW/text() &gt; CE_DEVOLD/text()">
						 <td style="background-color:#00ff00" align="left"  >
                          <b>
                            <xsl:value-of select="CE_DEV"/>
                          </b>
						 
                        </td>
						
						 </xsl:when>-->


                          <xsl:when test ="SPrintStart/text() &lt;= CE_DEV_DEPLOEDDATE/text()">
                            <td style="background-color:#00ff00" align="left"  >
                              <b>
                                <xsl:value-of select="CE_DEV"/>
                              </b>

                            </td>

                          </xsl:when>

                          <xsl:otherwise>
                            <td align="left">
                              <b>
                                <xsl:value-of select="CE_DEV"/>
                              </b>

                            </td>
                          </xsl:otherwise>
                        </xsl:choose>
                        <xsl:choose>
                          <xsl:when test ="SPrintStart/text() &lt;= CE_PPE_DEPLOEDDATE/text()">
                            <td style="background-color:#00ff00" align="left"  >
                              <b>
                                <xsl:value-of select="CE_PPE"/>
                              </b>

                            </td>

                          </xsl:when>
                          <xsl:otherwise>
                            <td align="left">
                              <b>
                                <xsl:value-of select="CE_PPE"/>
                              </b>

                            </td>
                          </xsl:otherwise>
                        </xsl:choose>

                        <xsl:choose>
                          <xsl:when test ="SPrintStart/text() &lt;= CE_LIVE_DEPLOEDDATE/text()">
                            <td style="background-color:#00ff00" align="left"  >
                              <b>
                                <xsl:value-of select="CE_LIVE"/>
                              </b>

                            </td>

                          </xsl:when>
                          <xsl:otherwise>
                            <td align="left">
                              <b>
                                <xsl:value-of select="CE_LIVE"/>
                              </b>

                            </td>
                          </xsl:otherwise>
                        </xsl:choose>

                      </tr>

                    </xsl:for-each>
                  </tbody>
                </table>
              </td>
            </tr>

            <tr>
              <td colspan="2">
              </td>
            </tr>
          </tbody>
        </table>
        <br>
        </br>
        <br>
        </br>
        <font colour="001122">
          <center>
            <b>
              Contact Details
            </b>
            <table width="50%" border="2" cellspacing="0" cellpadding="0">
              <tr id="contacts_line1" valign="middle" style="display: block;background-color:3C3B61;color:FFFFF1">
                <td align="center" valign="middle">
                  <b>
                    Email
                  </b>
                </td>
                <td align="center" valign="middle">
                  <b>
                    Location
                  </b>
                </td>
                <td align="center" valign="middle">
                  <b>
                    Extn
                  </b>
                </td>
                <td align="center" valign="middle">
                  <b>
                    Mobile
                  </b>
                </td>
              </tr>
              <tr id="Tr1" valign="middle" style="display: block">
                <td align="center">
                  <a href="mailto:HSC_DEVOps_Rewards@in.tesco.com">
                    HSC_DevOps
                  </a>
                </td>
                <td align="center">
                  A1-128
                </td>
                <td align="center">
                  84321
                </td>
                <td align="center">
                  9739097294
                </td>
              </tr>
            </table>
          </center>

        </font>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>