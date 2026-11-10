<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="/">
        <html>
        <head>
            <style>
                table { border-collapse: collapse; width: 100%; }
                th, td { border: 1px solid black; padding: 8px; text-align: left; }
                th { background-color: #f2f2f2; }
            </style>
        </head>
        <body>
            <h2>Студентські гуртки</h2>
            <table>
                <tr>
                    <th>Назва</th>
                    <th>Факультет</th>
                    <th>Департамент</th>
                    <th>Час</th>
                    <th>Староста</th>
                    <th>Курс старости</th>
                    <th>Тематика</th>
                    <th>Для яких курсів</th>
                    <th>Керівник</th>
                    <th>Викладач</th>
                </tr>
                <xsl:for-each select="Clubs/Club">
                <tr>
                    <td><xsl:value-of select="@Name"/></td>
                    <td><xsl:value-of select="@Faculty"/></td>
                    <td><xsl:value-of select="@Department"/></td>
                    <td><xsl:value-of select="@Time"/></td>
                    <td><xsl:value-of select="@HeadStudentName"/></td>
                    <td><xsl:value-of select="@HeadStudentCourse"/></td>
                    <td><xsl:value-of select="@Topic"/></td>
                    <td><xsl:value-of select="@TargetCourses"/></td>
                    <td><xsl:value-of select="@CuratorName"/></td>
                    <td><xsl:value-of select="@IsTeacher"/></td>
                </tr>
                </xsl:for-each>
            </table>
        </body>
        </html>
    </xsl:template>
</xsl:stylesheet>