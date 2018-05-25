<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
   <xsl:param name="SiteUrl"></xsl:param>
   <xsl:param name="MediaUrl"></xsl:param>
   <xsl:param name="DetailLink"></xsl:param>

   <xsl:template match="*">
        <table style="width:250px;padding:0px;margin-bottom:4px;">
            <tr>
                <td style="padding-right:5px;vertical-align:top;">
                    <a>
                        <xsl:attribute name="href">
                            <xsl:value-of select="$DetailLink"/>
                        </xsl:attribute>
                        <img style="max-height:60px;max-width:60px;">
                            <xsl:attribute name="src">
                                <xsl:value-of select="$MediaUrl"/>
                                <xsl:value-of select="./Pic"/>
                            </xsl:attribute>
                        </img>
                    </a>
                </td>
                <td style="width:90%;vertical-align:top;">
                    <div style="margin-bottom:4px;font-weight:bold;font-size:11px;overflow:hidden;text-overflow:ellipsis;">
            <a>
               <xsl:attribute name="href">
                  <xsl:value-of select="$DetailLink"/>
               </xsl:attribute>
               <xsl:value-of select="./Title"/>
            </a>
         </div>
                    <div style="font-size:9px;line-height:11px;height:33px;overflow:hidden;text-overflow:ellipsis;">
            <xsl:value-of select="./Desc"/>
         </div>
                </td>
            </tr>
        </table>
   </xsl:template>
</xsl:stylesheet>
