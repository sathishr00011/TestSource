<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

	<xsl:param name="prefix" />

	<xsl:variable name="prefix-no-core">
		<xsl:if test="$prefix != 'core'">
			<xsl:value-of select="$prefix" />
		</xsl:if>
	</xsl:variable>

	
	<!--<xsl:template match="Product">
		<HTML>
			<BODY>
				<TABLE BORDER="2">
					<TR BORDER="2">
						<TD>Group</TD>
						<TD>Component</TD>
						<TD>State</TD>
					</TR>
					<TR>
					<xsl:apply-templates select="Component"/>
					</TR>
				</TABLE>
			</BODY>
		</HTML>
	</xsl:template>
	<xsl:template match="Component">
		<TR>
			<TD BORDER="2" style="color:red	">
				--><!--<xsl:value-of select="local-name(parent::*)@Name"/>--><!--
				<xsl:value-of select="../@Name"/>
			</TD>
			<TD BORDER="2">
				<xsl:value-of select="@Version"/>
			</TD>
			<TD BORDER="2">
				<xsl:value-of select="@State"/>
			</TD>
		</TR>
	</xsl:template>-->
	<xsl:template match="/">
		<html>
			<body>
				<table style='text-align:center;font:tahoma;font-size:15px;'>
					<tr bgcolor="#66a9bd">
						<th>Group</th>
						<th>Component</th>
						<th>Manifest</th>
						
					</tr>
					<!--<xsl:for-each select="ReleaseManifest/Applications/Product/Services/Service/Component">-->
            <xsl:for-each select="ReleaseManifest/Product/Services/Service/Component">
						<xsl:choose>
							<xsl:when test="@State='ToBeInstalled'" >
								<tr bgcolor="#9acd32">
									<td align="left">
										<xsl:value-of select="../@Name"/>
									</td>
									<td align="left">
										<xsl:value-of select="@ComponentName"/>
									</td>
									<td align="left">
										<xsl:value-of select="@ComponentManifest"/>
									</td>
									
								</tr>
							</xsl:when>
							<xsl:otherwise>
								<tr bgcolor="#d5eaf0">
									<td align="left">
										<xsl:value-of select="../@Name"/>
									</td>
									<td align="left">
										<xsl:value-of select="@ComponentName"/>
									</td>
									<td align="left">
										<xsl:value-of select="@ComponentManifest"/>
									</td>
								
								</tr>
							</xsl:otherwise>
						
							</xsl:choose>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>




</xsl:stylesheet>	