//$(function () {
/*标准*/
//function initechart0() {
//var data = [];
//var labelData = [];
//for (var i = 0; i < 24; ++i) {
//    data.push({
//        value: Math.random() * 10 + 10 - Math.abs(i - 12),
//        name: i + ':00'
//    });
//    labelData.push({
//        value: 1,
//        name: i + ':00'
//    });
//}
function initdlechart() {
    juniorservice_radar = echarts.init(document.getElementById('juniorservice'));

   
    radar_option = {

        //      title: {
        //          //text: '24小时用电量分析',
        //          left: '50%',
        //          textAlign: 'center',
        //          top: '0%',
        // color:'white',
        //      },
        color: ['#f845f1', '#ad46f3', '#5045f6', '#4777f5', '#44aff0', "#45dbf7", "#f6d54a", "#f69846", "#ff4343",],

        legend: {
            data: [],
            show: true,
            type: 'scroll',
            //right: '15%',
            bottom: '0%',
            //y: "0",
            textStyle: {
                color: "#999",
                fontSize: '12'
            },
        },

        series: [{
            type: 'pie',
            data: [],
            roseType: 'area',
            radius: '55%',
            center: ['50%', '45%'],
            itemStyle: {
                normal: {
                    //color: '#F88E25',
                    color: function (params) {
                        var colorList = [
                            "#a71a4f", "#bc1540", "#c71b1b", "#d93824", "#ce4018", "#d15122", "#e7741b", "#e58b3d", "#e59524", "#dc9e31", "#da9c2d", "#d2b130", "#bbd337", "#8cc13f", "#67b52d", "#53b440", "#48af54", "#479c7f", "#48a698", "#57868c"
                        ];
                        return colorList[params.dataIndex]
                    },
                    //    borderColor: function(params) {
                    //            var colorList = [
                    // "#a71a4f","#bc1540","#c71b1b","#d93824","#ce4018","#d15122","#e7741b","#e58b3d","#e59524","#dc9e31","#da9c2d","#d2b130","#bbd337","#8cc13f","#67b52d","#53b440","#48af54","#479c7f","#48a698","#57868c"
                    //             ];
                    //             return colorList[params.dataIndex]
                    //         },

                }
            },
            labelLine: {
                normal: {
                    show: false
                }
            },
            label: {
                normal: {
                    show: false
                }
            }
        }, {
            type: 'pie',
            data: [],
            radius: ['58%', '80%'],
            zlevel: -2,
            center: ['50%', '45%'],
            itemStyle: {
                normal: {
                    //color: '#5DD1FA',

                    color: function (params) {
                        var colorList = [
                            "#a71a4f", "#bc1540", "#c71b1b", "#d93824", "#ce4018", "#d15122", "#e7741b", "#e58b3d", "#e59524", "#dc9e31", "#da9c2d", "#d2b130", "#bbd337", "#8cc13f", "#67b52d", "#53b440", "#48af54", "#479c7f", "#48a698", "#57868c"
                        ];
                        return colorList[params.dataIndex]
                    },
                    // borderColor: '#47A0FF'
                }
            },

            label: {
                normal: {
                    position: 'inside',
                    color: '#45dbf7',
                    fontSize: 8,
                }
            }
        }]
    };



    juniorservice_radar.setOption(radar_option);
    //}
}
    /* 飞鸟尽*/
    
   function initechart() {
      graduateyear = echarts.init(document.getElementById('graduateyear'));

      graduateyear_option = {
         


          title: {
            text: "",
            x: 'center',
            y: 'top',
            textStyle:
            {
                color: '#fff',
                fontSize: 13
            }
        },
        tooltip: {
            trigger: 'axis'
        },
        grid: {
            left: '3%',
            right: '8%',
            bottom: '5%',
            top: "7%",
            containLabel: true
        },
        color: ["#72b332", '#35a9e0'],
        legend: {
            data: ['当日订单完成率', '配料温度实际值'],
            //data: [],
            show: true,

            right: '15%',
            y: "0",
            textStyle: {
                color: "#999",
                fontSize: '10'
            },
        },
        toolbox: {
            show: false,
            feature: {
                mark: { show: true },
                dataView: { show: true, readOnly: false },
                magicType: { show: true, type: ['line', 'bar', 'stack', 'tiled'] },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        calculable: true,
        xAxis: [
            {
                type: 'category',
                //type: 'datetime',
                boundaryGap: false,
                //formatter: function () {
                //    dateTimeLabelFormats: { second: ' %H:%M:%S' }
                //},
                //data: ['2022年', '2023年', '2024年', '2025年', '2026年', '2027年', '2028年'],
                data: [],
                splitLine: {
                    show: true,
                    lineStyle: {
                        color: '#2d3b53'
                    }
                },
                axisLabel: {
                    textStyle: {
                        color: "#fff"
                    },
                    alignWithLabel: true,
                    interval: 0,
                    rotate: '15'
                }
            },
            {
               show:false,
                data: [],
               
                
            }
        ],
        yAxis: [
            {
                type: 'value',
                splitLine: {
                    show: true,
                    lineStyle: {
                        color: '#2d3b53'
                    }
                },
                axisLabel: {
                    textStyle: {
                        color: "#999"
                    }
                },
            }
        ],
        series: [
            {
                name: '当日订单完成率',
                type: 'line',
                smooth: true,
                symbol: 'roundRect',
                //data: [1168, 1189, 1290, 1300, 1345, 1256, 1045]
                data: []
            },
            {
                name: '配料温度实际值',
                type: 'line',
                smooth: true,
                symbol: 'roundRect',
                data: []
                //data: [1234, 1290, 1311, 1145, 1045, 900, 673]
            }
        ]
    };
    graduateyear.setOption(graduateyear_option);
}
    

function inittemechart() {
    courserate = echarts.init(document.getElementById('courserate'));
    //option = {
    //         tooltip: {
    //             trigger: 'item',
    //             formatter: "{a} <br/>{b} : {c} ({d}%)"
    //         },
    //         legend: {
    //             orient: 'vertical',
    //             right: '0',
    //             y: 'middle',
    //             textStyle: {
    //                 color: "#fff"
    //             },

    //             formatter: function (name) {
    //                 var oa = option.series[0].data;
    //                 var num = oa[0].value + oa[1].value + oa[2].value + oa[3].value + oa[4].value + oa[5].value;
    //                 for (var i = 0; i < option.series[0].data.length; i++) {
    //                     if (name == oa[i].name) {
    //                         return name + ' ' + oa[i].value;
    //                     }
    //                 }
    //             },
    //             data: ['test1', 'test2', 'test3', 'test4', 'test5', 'text6']
    //         },
    //         series: [
    //             {
    //                 name: 'FK',
    //                 type: 'pie',
    //                 radius: '65%',
    //                 //roseType: 'area',
    //                 avoidLabelOverlap: false,
    //                 labelLine: {
    //                     show: false
    //                 },
    //                 emphasis: {
    //                     label: {
    //                         show: true,
    //                         fontSize: '30',
    //                         fontWeight: 'bold'
    //                     }
    //                 },

    //                 color: ['#27c2c1', '#9ccb63', '#fcd85a', '#60c1de', '#0084c8', '#d8514b'],
    //                 center: ['38%', '50%'],
    //                 data: [
    //                     { value: 335, name: 'test1' },
    //                     { value: 310, name: 'test2' },
    //                     { value: 234, name: 'test3' },
    //                     { value: 135, name: 'test4' },
    //                     { value: 234, name: 'test5' },
    //                     { value: 234, name: 'text6' }
    //                 ],
    //                 itemStyle: {
    //                     emphasis: {
    //                         shadowBlur: 10,
    //                         shadowOffsetX: 0,
    //                         shadowColor: 'rgba(0, 0, 0, 0.5)'
    //                     }
    //                 },
    //                 itemStyle: {
    //                     normal: {
    //                         label: {
    //                             show: true,
    //                             position: 'inside',
    //                             formatter: '{b}'
    //                         }
    //                     },
    //                     labelLine: { show: true }
    //                 }
    //             }
    //         ]
    //     };
    echarts.registerMap('hjkj', {
        "type": "FeatureCollection",
        "features": [{
            "id": "500101",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@NFPGFIJEHIHMJGBKLQD@D@BB@LFBHADHDDD@RJLBDJDAJEDGD@NHB@DGACBAFAHAHC@CEGO@AAHSBM@IBCJCFEDGAM@CDAJ@VHBHAFDBDABGHIHBHAZ@LFDGH@DCBAN@HEJALJRJFLJFLJLJHBN@DBBDGHADHVDH@DEDDDJHTJ@ACKDENKHEH@FHLJJBhVHFHJVL`LTLRDJLTFBCD@@FEHGDMFM`ERNAJCJ@FFLDPJXRJNNLHF\\FBBX@NBJFD@JGLBFCDE@ECEAGDGBWBAHAFHDBN@XFFDHEJANDDD@FEJ@DDFFFJ@LC\\JJBFABA@CKEAC@CBG@ACEOC@EHIBGNKDABIDKDEJG@ECGJSDMDEJAV@XFNDPLZ^BBF@LGBC@KHEJKII@CDCEGBWCKFWBAD@PJBDCDHHZDBDDADEHUAILEBCHMDQJAFENGBCCKFCRQDG@EACFIBAFBVFBHD@DCACZ@BALKBGCKFUNGACKGACFQDCBIDKBKDGJID@HDH@DADEAGKIFIDA@AGM@CFANBbBFACGDEYUBKCGDCDAHB^JfJFABAJYHADBBABBBBD@PDD@BDH@BFLA@DFBB@DBBAHBDDDADDABBBJBB@@EBKB@DBBBDEF@FCHMBYBABAAKAABQEE@EGAAADGOC@EKAAGCBEEBEACI@DEGGrgNENKDC_]AQGG[cKK[MCCBECGMMMIYGFCNELKLGBcSE@KDKAGCCIIEIB[PGHBHDDADMHKDADAHBDFD@DCDAFKBADEBGFBHAF@HCFFHIRCNBHEJGBE@]BM@OEQIGECEHYDGHG@ICCDKAGEACEAAUEGIGBGE@CHAEIUGK@BILA@A@C[QGBG@BMM@EC@ECEGAIGCKEAA@CDBHCFIJE@CDABB@JBADSBGFOAFCDCAACBKJAAACAACFMJIJKFM@SCECEIGGGCKCE@KBGFGCBLCDCACAIEE@CA@GAAENABICGBACBEEBCHAB@HFNFHDHA@QGGGEAAHBHALC@IAGDAFAHDFFFLDBF@PADCDEBIIEICCIAMDIEmIMM[KMGSAS_a@GAGIEGMOMEOCAE@WLG@CAW_CCELGTCFGBWBABAHCDSLFLLLTXCFIJEHAJBZAFojU^EFSFGCIKCBCFAHIH@DLV@JEDE@WKA@@TEHKbIDMGK@EFAFENL@BB@JBDAFMFKACHIAGI@KAGDE@ACAE@GDC@ICC@CD@HABOFIAIEEGMEMBQFKCGHEL@HDBHB@FGLIJOvAJPDBJBPDFZTHBTENBR@THDJ@DCFFF@BEDBDGJBD@JED@FHHDLLLYPKDUAS@EE_OOCSAOBeJqFCDOPEJSREHE@IJCFMJOFCDIL@HDH@DGDGRSZGJOLRNFHFNJHdXf^TH\\RBFAJ@DPJ^PLJFBLBDD@H"],
                "encodeOffsets": [
                    [110587, 31651]
                ]
            },
            "properties": {
                "cp": [108.380246, 30.807807],
                "name": "万州区",
                "childNum": 1
            }
        }, {
            "id": "500102",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@DEJGNQFC@C@CB@NHBBDAFE@CAAAGBCHGB@VHBTJNDDFCBELKBBDBDEH@JCBECELGAEBABAFABCF@DCJDJFJ@FMAG@AGAACAENQNGFADEBBF@DCBEFGAKFKGQNIJKbEUQEKBCAAQGCSKEEEDCNADC@]AE@ABKLGJCJ@HDRPJDBE@]AEAGBaFOFAEAEDEKEAEGGABCHAFKJEBEAUCIGGBKEW@EFELEFI\\ATQFADC@OFGXFHAPIfGF@BAEKEOYiCC@ADABCECDCAGEEGCEEG@AIGCACEGEIIGEI[YCQSMWK[GEBGCAGAIDMCGHCHADEBC@MBCBCACDU@CSEOBGBE@EAY_ECCG@EAE[@MGECCKEG@IIEKIEACJOXI\\GNQPOTa^EJCRG@ICGAACBEOGIIGBCFCACABISECBBFITBTCBEAE@@DBDCFODCHIHOF@FIBGEGCEEKCC@IDUOS@SEG@EACC@GEIYYAOKO@GAEIIc]OKMEE@EAEE@CDA@AMEQCEFGAAAAEQGACFC@CEG@CFCNQH_BAACDSEEM@QFaACBBBJJEDADANBDLTBFABK@GEE@ABDRFBBBCFE@EDIEMDA@CAFCAAC@ADCBEAECA@CBCBEVKFAA@EGEC@KDCECCQE@AFCJABABCAI@AF@@ACEGCKDEAKGAEA@ODC@CCCDDDCBMI@AFIDFFQDKBC@CECWCO@OCSDCBELM@EHI@KFIDABDDADBBBDF@@DFD@BBDCDC@ICE@CD@FABCC@GCAKJEJ@DJBHH@BEAABCJ@HE@@DFDEHC^BDPNDFCX@DDDEFGBMAECABAH@LBHFH@BCBMCCEBIGMCAI@ABDHILUHIFG@OBIAABAFIBEDOBD^FNF^@pDVFT@LPbF^DFH\\@HDJLVBBF@BBELFHBPJTBNJ`FZLCXJN@RPBJHDBFVFJNHBLJHBADBLP@NFdORRD@^RPHPJRHFAH@NJHBFD\\FBD@DLHBFFJ^L@DGDCF@DFJCHBDBBDHL@JFN@HADAL@DDDDJA@BADDBD@BEJ@FBGLDBLKHBBFB@DEBKECAAFCZDHCHADBFD@HDJHBJEH@VNHHFRDBBBADEB@DPBBB@FADCD@D@BHFBJDBFCB@FFTDPRFDLIDID@@DCZPPDFADED@DTVVbX^FDBCBEBBPPDJRPPVFBFBBCFALBHABGHCAE"],
                "encodeOffsets": [
                    [110051, 30710]
                ]
            },
            "properties": {
                "cp": [107.394905, 29.703652],
                "name": "涪陵区",
                "childNum": 1
            }
        }, {
            "id": "500103",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@DE@OCMIKMCcGa@EAGGCEAISBADAFA@AC@CAAO@CBA@ACCACBABAFBFDBIDBFCJCBYBAB@BXD@FRDTCNCXAJFFFFJHHZBHALGN@D@LFHFDD"],
                "encodeOffsets": [
                    [109147, 30281]
                ]
            },
            "properties": {
                "cp": [106.56288, 29.556742],
                "name": "渝中区",
                "childNum": 1
            }
        }, {
            "id": "500104",
            "type": "Feature",
            "geometry": {
                "type": "MultiPolygon",
                "coordinates": [
                    ["@@DCBCH@AKDCDBBDFBBA@CD@BFCFDDDAACBEFDDADACAFEDBHJADCRFTBLF@BBGDADEDED@DHLFTDBFCHFFA@DENED@BEAAEIDAFBFAD@FD@ACFC@DFHN@AFH@FAH@@AAEHED@BBCDE@@DD@VGDBFFD@@IFCBD@FBDB@BCDALCLED@CFJKCIODI@MICMCc@KDIDEjqHEN]BIEGA@OD[PKBQAMAeO]QKMEMGOGGGEUGWKUC@LEJAPD@J@LREDCZMHEHBBD@@DEJCVBFHBBBKNBTFJ@FBDJCNDBAAE@CNEAMBEFCF@BDABADDDB@"],
                    ["@@EDHBAE"],
                    ["@@@B@A"],
                    ["@@@@@@"]
                ],
                "encodeOffsets": [
                    [
                        [108979, 30133]
                    ],
                    [
                        [108965, 30147]
                    ],
                    [
                        [108979, 30134]
                    ],
                    [
                        [108978, 30134]
                    ]
                ]
            },
            "properties": {
                "cp": [106.48613, 29.481002],
                "name": "大渡口区",
                "childNum": 4
            }
        }, {
            "id": "500105",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@iIKIIEIQmCCG@IDOPSLI@MEE@OBIFCJ@HCFCBoLYAE@GGMaEEMEoMOIGIGEOCG@KDCDUjGJABGAEEOCGDIHGLAJANCFCDCBIDG@QAWKECIKEMAKBWDIJECCGEKEG@I@KHGBYAGGEIEEIEI@MBMDODUCK@MNchQFEDAFBDNPFRDBBDF@ZADBCDDBHGAABEHBFMECG@DMAEEDBCDIHA@A@CNMDBEJBBD@@E@@NGLHDFD@@AFCBD@BAB@BF@BDNHHADDD@BA@CNCFCD@BBAFN@JHBHBHAN@DFHBDD@FFDLLAHVAFDDH@DBDDDBJCDAAABAD@DHNDDA@CBAHBADH@@AACBANBLAB@DDNE@BHDBCAADEBBDFJ@HEHEJCH@ADDBNCDEJCBHFDDCH@D@BDHAFDHPAPBBNEHGFLJBADBB@FHZ@DD@FGDAFPBBDEAEFC@ICCBCDAJCHJBELABB@FBBPBNHHJABCD@BHJHDNAHIHAJHN@FADCEOHID@LFDBTENADCJEDCJCDGBBDFJJHHPBBAAGDCHFF@"],
                "encodeOffsets": [
                    [109460, 30369]
                ]
            },
            "properties": {
                "cp": [106.532844, 29.575352],
                "name": "江北区",
                "childNum": 1
            }
        }, {
            "id": "500106",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@ZUBIEMIIQKGM@EDITGBEEQEMKM@G@AFCREnsHCHBBCAAGAOABCE@@ABABCDKA@GAABAF@FCBAKBGQAKEBGACGAAEEICGAACCDGCA@IC@A@CKM@ABAFI@EA@ECAG@EAS@AJ@HBBF@@BBDCBABFFDv@DBDOBA@ACE@EJ@HCDCCAEBEBA@GC@EFA@GCAEG@EDAD@BDBADBHCBICAJIBKFEHCBAABAACA@CFA@MGA@BAGCAGIGIB@EGHOEIDAGCAADAAC@CCECA@CHABECAQAAOD@CCAMBGEGAECEDAJGBBCCAAFC@CNBnDRHADBGJFTBNN`FDD@DBHTJJABEBGDCDJLH@LAFDCFMDCF@PBDHBFPEHAPGAAB@R@LADFD@RBADGFADBAFBFDBBCF@DDTD@FDN@BAAGFC@ALCD@BHBBp@DBADBBRCD@DF@JCFBBLEB@@BDBHCB@BFRG@EACDCBFJDH@PEB@FHDBAFDBBFD@LGHDHBFANIA[GKAKGMMFcIIdGDEF@DB@NBHDFLF"],
                "encodeOffsets": [
                    [109013, 30381]
                ]
            },
            "properties": {
                "cp": [106.4542, 29.541224],
                "name": "沙坪坝区",
                "childNum": 1
            }
        }, {
            "id": "500107",
            "type": "Feature",
            "geometry": {
                "type": "MultiPolygon",
                "coordinates": [
                    ["@@DJILDEC@KFKDCBADA@AC@EACED@JC@EECAUHC@@CF@DCAAC@GFBF@BG@EBG@BEM@EG@CEDBDC@@EBCAEBEJCBFFB@AFCFM@CEBGEEDCAESGK@CFCFCBCHCAAE@AKESDQBCIIA@EDDBCBCBECAFBDCBCCDEAEC@@DABEAACCACDBLG@ADCD@DEGDGCAGBCD@DBNKDAF@FDHEA@ACCGAIDAC@EEKAQLMAAGAAEDUFI@CC@AAFGNGDYFCKQI@C@BOFI@KMCAGASIGAC@MNaAIGIGEWEWAICMGSQYI]ISBGBEJELMXKNDHFDETFDFDCLBBDF@HBDGFCACCIDABAJQDEDCFFF@JDD@DBB@DVFDDAHED@DBDLH@BBFALBLDFF@HADC@GBCPDJABBCD@DDDF@DFDADCHBFLNF@BEB@`ABM@AAAGABAJDFJFAJCDC@MAGAAPCHGCEJA@ECEFCAAGC@AD@HIDCHKDIACBEBBFGP@D@DFDCJBHDDBBLCDBBDCFKACDFP@DC@CICBADF`JXEBAECCQF@TADBDD@BEDBADHABIFCFDHBHFNADB@DPCBBBRFDBADGB@FDDDD@BBBCDBBHJCPFHG@FJAJHBHHDABB@NHB@DEB@@HFAFGNGH@BIJDDAAGBCCA@ABCFCH@BFHDB@FED@@HABAFDHBBDC@GFIF@BDB@PAAC@CCuEEBADAAC@AG@@IBIT@FBJ@BB@FFBJ@BEBAN@DLB@D@@JDBCHDDBBDHFJBFHBBDAHLFRBAHBLDA@EBEBAHBB@CLADAB@B`ADABE@IJCCAAEBEBADADBBDB@DAP@BB@DBDB@BEBCTA@cBKLGVKBEBKCIAEGIEAI@EBGHQD"],
                    ["@@AA@BEADDDA"],
                    ["@@@AA@BB"]
                ],
                "encodeOffsets": [
                    [
                        [109079, 30188]
                    ],
                    [
                        [108965, 30147]
                    ],
                    [
                        [108979, 30134]
                    ]
                ]
            },
            "properties": {
                "cp": [106.480989, 29.523492],
                "name": "九龙坡区",
                "childNum": 3
            }
        }, {
            "id": "500108",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@ACMMKGKUAECCOW@EM_EEMW@EACIKMSGSGCKB[CEBGCC@BFADCBUBACBC@CEEMG@ADIHCECKCGGIEKB@BFFCBI@EJGEBEA@GDGDGHC@OCICCDI@CAGBEBBDADCBG@IEC@CCAGCAQRC@@AIEENBDGFE@CJSFC@ACG@ALAFULKHAF@F@hFLFFHD\\@jHNDJLDN@LAJIDELAXBLHPHJ^PTBF@NEHIBQJQNIDAJBLHHBBAHIViDCLCH@PDHFHJPJpNNFFFNbBDHD^BpKDADE@GDIJE"],
                "encodeOffsets": [
                    [109365, 30300]
                ]
            },
            "properties": {
                "cp": [106.560813, 29.523992],
                "name": "南岸区",
                "childNum": 1
            }
        }, {
            "id": "500109",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@POdUFEBE@EGMG]@CLGFI@EEOBEJKGCAM@WFKMm@KDWEKBCB@ZFB@@CACG@ICDC@GIGIAAMDAH@FCBI@GECKDC@AAAE@EFKACEHC@AACA@EDG@GCAEB@ABIGCIGE_EECAEDIOAAEBADCBE@EAAAACDIBC@AC@GDG@WEACCCBK@EGAIBEGAGCSE@BCNBJD@DCH@FDALORABA@CE@IMCBE@DCAEIBAATSFOCAO@IGCEDGAAGDCAIDCDEBCDGCACAAA@EFAEGA@CCAE@@CCAA@CDKBIGGCAB@JABCAKK@GHGHEJKBG@IEGGEWGIIAGFSEKCCEAUJGJKJC@G@WEMGIKEWBGDEKECEAG@MGAA@CFcHJJEdNHNBLHLB\\CDC@EFEBGAIAIFC@AECABECAEGA@OFG@ICAECDBD@FQHAEA@GDCA@AA@KFAADE@ICEC@QDAABCCAo@AAAGC@KD@BEDBHABM@ECC@DNJNADHN@LBDHFFHDDFADCFCJDLN@FJFpzSAKDE@EEC@EBGHBDAHBLBHFDADKGEBLV@BEBGACFG@BFHJBD@DCBIAEJBFAFADMLAFGHBDRH`V\\PFDFFPHZPHDJGFEBKCEBKDIFETKFGJCNEJ@BBADJLCF@LG@AB@DDDHBDABGBAJ@DBHFPb@BEDDBJ@LA@AIY@ACBCE@AD@@AIUAAEAECBAF@D@ACCCCCACBADBDACEBCLBJNBDHEEGIOCE@GB@B@FDPFDFBFCHBFFFJBDJABBDE@ABF@DDBJEDDBBDFBN@FBBDDLHHFBFHDJHCNED@RjPGTOBBVtHZPb@NFL@RDFRN\\RFF@JABKHAHDFpfJFFHFFXJHDL@"],
                "encodeOffsets": [
                    [109209, 30808]
                ]
            },
            "properties": {
                "cp": [106.437868, 29.82543],
                "name": "北碚区",
                "childNum": 1
            }
        }, {
            "id": "500110",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@GDAFSBAFJLBXCFMJC@KEI@OHCDCLC@CCAKGGKCC@KJM@GHANBDHF@@CBADIEGDE@UIINKFEFE@KCEA@CBCLGBOCEA@O@OGKAEBAD@HBHCHBHFDBDABKDAFBFJJ@BFFRBNCDBDD@JBBRHDBBF@DDDVJDBB\\CLCVKX@FJ\\FDDANGHAHDDDFH@BALDLCFAFKA@JSFCHBFFDF@XCPBNDDFABGTIHC@IKGAIDMJCF@JGJ@FJLBB@DCDE@QKGDCFCDCBE@MIBHITKBGD@BBF@BG@SXAVJ^ANCJYXC@KAMDAFDDAFEBK@CB@JCB@FDBABAB@DC@CAGDI@ONEBEDGBAFGLQTERKBGCECEHABCBCFCN@LCFGDBNUNOBEDMACBAF@L@FGLBRCTAPBJObL`ADIHEFHRDP@FENIHCD@NALIJFH@HABIFEF@BHBNAJCNLEFFFVPJDLALEHABEHACR@DD@JCFAFEVC@AECYAI@AAHI@IHGHKJCVALEDFBHDDEHDDDJNLBF@JEPEVJTCH@DBFDBXMAAEA@CJIDIHCLAHGDMJIBCAGBCf@BCBALO@AIIBEHEGEAA@CD@@ED@HBBA@@DJBBDBDHDANNHABDDALQBIDCJ@VHFFFDA@CBK@IDKXGpHHCFGVCD@FFH@DMXBHFZADIDADDRCHPHFABGDARFD@FCFANH@N@BLHEHNNDNHHPLFAH@DDCFBBN@F@BDDAE]B@JBF@HED@DFANBBDAFLDFFA@EB@FHDTBBJCHELMD@@DVBHEBEXUJFDDBFRHFFPEACDCLADBB@AKDED@PFPTFFF@DMBAFDALBDFBJAHADA@IBKJKDQDCHAH@JHRDHDFBFAHEFKF@DFFBbQDADKJIJOROLAHEFGFO@CAEACGAHGGI@GEC@CGCDICEEA@ADCCK@IDAB@@JDBFAFCBCDIJADDD@JIBFFBFCCA@AD@JFGH@BD@B@@JFP@FBFFFDID@BACCBGBCFADDF@BEHCCE@AD@FFHEDDCFBBPEBACALOMEAA@KACJE@EJIDADBFATKNODIDEPEJQ@IGACKCAI@EGBICE@EFOH@FKFCFEL@FBFFDBLCNIDK@GCEAIACE@KFAABEFCFI@GDC@EFEFIMICGAKEIFCVIO@YIABAFDVCFUDSAAIDGDQ@IECUBAECCIAOBMEFSAEFGBICGCA@EAC@EEG@C@CBCAEDEBACCFA@CDA@CDCBG@GCC@KGCMAABOBGGEAMCE@ABAFBNCBIDEFADCNADGJCHA@GLEBECGGIEGGCAOJC@ECUQOUIGIAC@@JCBOAOBIGAQACIFAAQW@CHSBMDGDAJDBABENCJET[DIOACC@EDIJILCLHN@JALF@GCEAAGBACAE@GGMAIGSIK@EFAAWCMBEAEICK@c@GBIAAG@CBAHEAEIM@GAAOAIMAAI@ENILKFEPBHMTGAGGAC@ICEQDIACCCC@CBMBC@GAG@CROHEJ@JEDG@EEGUCCACECAGBI@QIMBUWAKEMBMBGHMHQBANIBCDKBOAODI@E[ECCAEEGC@QDGCGGA@"],
                "encodeOffsets": [
                    [109269, 29134]
                ]
            },
            "properties": {
                "cp": [106.651417, 29.028091],
                "name": "綦江区",
                "childNum": 1
            }
        }, {
            "id": "500111",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@DBHNLJABCBIIE@G@EF@DFDFFBD@HFDBD@FAFABACC@AFBDDNNL@DELCDG@IF@DD@@BADSTCA@CIAMKOCAE@ECCGBBFABK@WLE@@DJDCBAJA@C@CCBGCBABEFAH@BBD@BCDDJ@DKHDHFLB@FEDBBDADBDABECSCECCFAJCFCDACACDGE@@CDCAKDCJABGCAIDA@DIECAEEJI@BJBFABEDE@EFADBN@DCDIFMBMEKAABDHDFADEFCBEAACI@BPBBHABBJ^@BGLADDFDBNF@DEF@DBHJ@@BEDBD@BGHGBDFAFABEBAFFD@DC@MCEFGBEAM@GAEAEKIG@CBAACECEAE@CDCLBDBJADEDEBAEI@BFABGEA@@BC@@EGAA@AFEHGTINBdHTCDKDDJQF@HGLEDAFDJDF@DCDMHENBJ@FABC@EBENBJHLFFJFNBDCFKFE\\GNCBCDGJIHBNB@FKHELNJF@DJRJ@BCBKDBNLJJMHGPEBBBHADADCF@DDDCBFDRDANDLIHAF@DDH^^AFE@EFF\\BH@JBDCHBBFBBDBBBCNENCJHBDADEFDDDADBCH@FFBBFFFDFFA@A@CHCJAVEFHPBHFFABCDAHHH@BFADBBPHHADAL@DKHEFAFDD@BCHCFB@DDBFFHD@BAF@HHDF@JDD@BCHC@WAEC@ABIEGOBAD@JIBIDA@CACE@GGIEEKKOCIDCD@JC@DJ@BDTJJBHHDCD@BB\\MD@PRDABB@FDH@FFEBEAEDCB@BCHA@GF@NG@EDCDB@DJC@ECMBKDAF@RHBABEDCCIBCNEjAPBTAHFDHHJCDBFVBBBBHHLKJJJ@HADDDFAFBFIHEIIDIRBFDFAHBHCBEJBHGHCBAFCFHJALFRCF@HFFDF@FAF@DEFBBA@AFAACD@DC@IFCCWFABACG@ALCL@DBBHFEFBDDLCBAEGDCBEEKVOKKKOKWOSKSAGKYAAC@OLGDA@EOSeecC@C@CAKQCKBAEAW]AAEACGGCIIG@GICAEASSGOKEGGAE@CHIACECGAI@KFE@GEECOEKIQ[AIGQ@GEKCUCI@CIGBKCG@GCGCSECBA@CCICCG@IKGAGKC@EMKC@MFKK_ECC@CEIAAECDEEABCBBBE@@FEABFE@BBIFCDKB"],
                "encodeOffsets": [
                    [108267, 30091]
                ]
            },
            "properties": {
                "cp": [105.715319, 29.700498],
                "name": "大足区",
                "childNum": 1
            }
        }, {
            "id": "500112",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@@@EK@MCKDWCWDCDCVIZSBEAECODK@E@AJEBGTAVILCD@HLFBDC@M@AH@JHD@D@HENJLDBDBJ@@D@F@DCDBDADBBFJEDBL@BFFBAD@BLH@FBBBHH@BAFGHCDEDCHCF@DCCGFGCCIEAC@EBCDAL@FACW@GBGAOHKDALDAHDDLABAAEDAB@BB@LFBLEBCAGDCFAH@HGLA@DB@HBDDB@B@BADAJBFCAK@CDAJ@F@B@@GFMBALABFB@XEF[D@LBNIEGO[QWECSkKEAGDOCSOGCE@IBEJCDCAGACBADKACAM@ADA@CIQRMAEHCBACCG@CKLBD@BIFCBABOHDDHDA@CFA@EFGYcEOKIEGCIEcIUOOaIIEEEKWEIEIE@ECC@@JABOAGGIICEAACHIDCDIFCDMBSFCAKEC@GJFPCDEBM@ECACE@CBGJIBE@ECGI@ADCBAGIMGOAAA@EAAKBAFGIOFDH@JEDBHCDGQCBEHC@@CGY@EAABCIACKIHMFAABOGOECGBACC@G@CDECAGIDCFMDCABCG@IDGFGFI@CEAACFBBADGC@AMFCCA@KBMAABBD@BG@BCGAAB@DCBMCCGC@ABBBCBIDCACCCAG@CCBEGUKBCKEEC@ACEG@CBMAGAGMGI@BECAUH@DABC@CCCBE@KGACE@@ABA@AACED@BC@CEKGMH@@@FC@AAFICAMN@D@BGDELB@DCBFCNH@FDENGAAFBBGHCADCCA[BC@ACCADLAFCBKDGF@LBHJJNHDDFFFNAJWTEH@NDRJLNHXFH@D@LIHIVIFBDDFLETBHJJXHHFFH@JAHILGFGH@HLJBDDA@IBAHDJHLADCB@DB@DF@DB@DHBBFFEB@BBBDFDLEDCJCDBHCBBCHDFJHP@DBEPSTBBJABFCDF@DAJNF@@DABQBKPCB@EDG@CICMAADF@DTBHFHJAHB@FALDDBDXFH@HCD@@BADCJBDBBFBF@DABCFABBJPFCDBFFF`JHHDAJ@BFADB@HCH@FDBBBD@FGBDEL@FBFBBD@LCFD@HAJEDG@CBBNJBJH@HCDJDH@BD@DA@YEA@ADFLCX@LNnEL@XBNHDILAFFP@FEJKH@DH^HN@FAFEFcVOPJ@FBTXFJFTDT^D@AAGDC\\@"],
                "encodeOffsets": [
                    [109265, 30841]
                ]
            },
            "properties": {
                "cp": [106.512851, 29.601451],
                "name": "渝北区",
                "childNum": 1
            }
        }, {
            "id": "500113",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@EYI_AMISAOEGFKAAE@AAKUCI@GG[CEE]Oc@IESCU@iAOCSEMC]ACAMIW@MGGAI@MAIFO@SCQ@]EEBIAAEBIHA@@GC·BGAOBABCCI@EDICWHMEEBUBAPAFADU@WB@HHD@BABIF@FBHCBIBG@IBCAG@GVYD@JFFAKE@OAB@HCDMYMCCE@AFCBADDBGBAF@FBBCCAMBACA@@DG@AEKAAEEACBUAKDEGGDCPEB@FBFABIABEIEC@EDCA@AAOBEHAHAHBB@ACHKIC@EFKGEMEGQEGDKCIIJCLCBaREACEE@ELGFEBEAGCQCIGG@GBCDCRILAL@HADEBMBEAACBKECABCNE@EEOSOEC@CFBLA@CAKBCDBDOFEEQGAECCIEWVAFGFUA@CC@KNGFIDAACSEGA@@FEBCEEKCBAABMCEC@GFE@IAA@F^ABA@ACS@AADECCG@EBIEMMCMMMFGKG@AAEBGMGEBEDWEADAFCBGAECEACFK@IDUVALBJFDF@FBFF@DEBEFALU\\EH@DHRDZABIAEBCFBFABSFI@CJ@BADE@ABDFAF@FHTCDGBBLCBAF@HMB@DFP@FG@GHSFEFCHHPEBE@GVGFBNCF@LAD@JBRFDGLHHEBE@GEODCAGKCAYEA@GFNLHPFNLN^R`LNFXBLARKTGH@FHAJM^GFo|AJ@RDZDNNJJ@bGJGNAFBDFFJDJH@BDF@REDIF@DCDCAAFMJF@BD@RQDBBHDDD@HFF@JCAENCDBJ@DCJDPDD@HGHCHCB@AFHFFIJ@DAEE@AJAFBFDHHLDFDGDCJ@BNHFF@DADBDVADABCAED@HDFA\\DHAJDDBFRZd@FNXFFN`@FPXDDBFLVLHNNBDPAF@NFJ@TKPOJCH@DDRnFJJJJLjLTLXFFJFbJPPJVFdDJFHLJFPXbHFHFJBHADE@CFGBGGGAGDGBECGDALCBCAMDQFE"],
                "encodeOffsets": [
                    [109571, 30428]
                ]
            },
            "properties": {
                "cp": [106.519423, 29.381919],
                "name": "巴南区",
                "childNum": 1
            }
        }, {
            "id": "500114",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@F@DAJABCAE@C@CDAPBNGPFHABAAEGCHIJADE@GEE@ADED@BABIAGGCQBAABCFCLCJBNMDGAQA]BGFGAGGI@W@AHBFEDGBECIAKGKLSLGBC@ECGGGCEDGH@RHJFLDH@HG@EOMCEDEPELAHELANKDG@KLGHC@AH@@FD@BKBABB@HDHGJ@BFBH@HCFEDGJ@D@DD@DABK@EFADHTAHGHMFMEKTFB@DJDT@N@JAJADE@EGQ@CFENADGBOGKAMMHKI@GDKHCNBLBLHF@BKFCFAH@DBAFCH@JBHHJHNDR@FNCBA@CCEIG@AAGDEHAFDJDFHD@DADSDCBKFGD@JDDDJFNCFC@CBAHADCDICMYcIEAABCZMFED@FLJAFDHTHKHI@AOMACLMH@DABCBCCAMGEI]SKOEKBENKBCBICKDI@UCGKGCEBAAAKACACKCUFEHK@CC@DS@AHAFKLCZBVIX@LCJENY@GJISWBKCCDAAGEGAEBSGIACDWAAE@CMKQDMBAEGGKGGAQGOBCVADEBIBCZMNEL@JCH@BG@AG@CAAKEEAGGICGGEGMICCKDISMaIEC[{UcKOMaIGBGCKFAI_AAGAQDABBDADECAB@DBH@BYHO@A@EJEDMBEACAACQCE@CBCBCCAABQCEGCiBACDM@GCAGBEAFEFABACEECKBGAECCEEAKDeVCFCNCFEDKBIEGAQFE@CA@EJOBCACKAAAHQBIAECAEAGJWJOESKKBF\\CDECEDELAJBJDF@BCBMAGEACHC@GEKEECEQLEF@FFFLFDDCFCJE@ACA@IHADE@AB@HBDABG@ECA@CJCHEFGDCDFDBLHBFFJXMTUZCFAJIZ@FBJFHDBLBHFFDDFBFELHRHH@DIT@FBFLLDHHFDHUbAFIFIBG@EJE@IHAH@FBNJRHHHXAHKRCFED@FBD@FGFBHFFDDBlDBVFPBFDP\\NVBFAFGFCNGHAF@FBFFF@DCHGFcRAAK@WDGAIDUDaPCAGECBADADBLEDAJDJJHDFFPBFJFLDH@^IBBAFFDNBBB@HBLGHCLKFCDBNATCDEBYT@LENEBGGABCDBHCFKDIBGACDFdLR@FBF@DBDH@DHBFAFCBBHFDVBHFFBFJ@F@B@DBB@FADCBUFMLPHBJAFEJCBGFABPFBFDBLMDFJFN@FBBB@DJBHDDFAHEHIFQDKDEHDNPVDLVbV\\FBJADFVNFHBHERBDRBBF@HDFPHFF@DNEHDRBDDBA"],
                "encodeOffsets": [
                    [111231, 30584]
                ]
            },
            "properties": {
                "cp": [108.782577, 29.527548],
                "name": "黔江区",
                "childNum": 1
            }
        }, {
            "id": "500115",
            "type": "Feature",
            "geometry": {
                "type": "MultiPolygon",
                "coordinates": [
                    ["@@LDFARYJYFGHERADEHSBCFAPCVBBFGJCJJPD@D@HCDODC@GHH\\JHF@HRR@DQNAB@DDBBDGD@DNXFBX@DABGDAFBAHBDPP@HEBAJBFFBD@JC\\LDDBFDPNJPR`VNLNPJNTRFHPLRTLJPXFGNCNITKJKBE@CA@DCF@HIBCPMJEJADC@GEIKMGKMO@CBIDED@\\DD@FIDEBOEMIGAGHSIMC@UFC@CI@CBEAEMACC@CAAKBKADGHEVKBA@I@C\\MBAEGLAFDN@ZKFEH]ICECNO@CACQ@GAEE@G\\KBCDIFAXUHALDPHJFL@RANEJGFDjnZXPLLRF@HFFND@VKHIFCNKQM@CDIKCOUQOCIMOCAAFADMKOUOYY]@CFCBCCEOODY@CC@CJKJECOQSCEEA@EDCAAIGE@A@CDCBC@EAAOA@CFABCAACAEQGGUMG@IFGACI@GECCAGBGDYCEDBBFDALCFA@AEGAKLCAHKEAI@AFC@CABC@AIBCCCCK@CBGBM@IEK@CGAAACDGEI@CDEHC@C]KEIAEKG@CAC[EECGAMIG@EBQGOIOG]QC@QQcPMEO@AKBCGAKIGAIMUEAEGCAIOMAAM@QGGAIDEFCRBLADKFCBDHAFCHBHFFBFAFEF@DAFEBGAICIEEH@FEB@DCBCGGCAPABEDAJC@KADLH@DDABGDBFQNJR@DCB@BBNBDCLABBDBHCDIDAF@JDFPHDTCPBHLFTlFDRXP\\FHMJKAC@E\\WFA@AEKBABEN@HA@E@I@CB@DBLEDIACBABA@A@CCGAA@@CKBGHG@EBCDBHADKFEA@KAAA@CBBFABKBCCBGMCINBPAH@HDXEBK@CBAD@FBDJFDDCDJBJDPBLFD@BABCBAJBNAD@HGDCF@DBBPDBTD^LD@BIB@DBDFBJFJDBDD@NKDABLFVZDDPD"],
                    ["@@A@A@D@"],
                    ["@@ABBA"],
                    ["@@A@@@B@"],
                    ["@@EEA@@FB@F@"]
                ],
                "encodeOffsets": [
                    [
                        [109544, 30806]
                    ],
                    [
                        [109545, 30811]
                    ],
                    [
                        [109549, 30810]
                    ],
                    [
                        [109546, 30810]
                    ],
                    [
                        [109628, 30765]
                    ]
                ]
            },
            "properties": {
                "cp": [107.074854, 29.833671],
                "name": "长寿区",
                "childNum": 5
            }
        }, {
            "id": "500116",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@BAJABCAC[OAC@ADCEGDCL@BCAEBAF@@AACEEOC@GFK@MNS@IDAFCXQFCFCFB@EHCBBBABCACB@@GFIH@BAIIFABCAAACLCLB@AECHABAECDA@CFE@CBACKFMHEBCD@FEDEDG@CG@ECCKECEASDECFMAKDC@CAGBIEICC@EBCAE@CEAGCBGCIBMHGFIJI@EFGJEVB@CGEEBA@CCBE@EAAK@GDAFCFEVELGDCAIBIFIJC@MS@EHIRODIFGBEBEZQJKBKACCAAAOHEIG@EEG@MBGFCNQFEACBABBLMFIJC@KCULEASDMJIDIAMMGADADIDABCGADGCAEAFGEEIFIIJG@CGKABC@GCADE@ADEHGJGDCBMMAAA@GLBLAJKH\\R@HK@LNBR@HAFIFBF@FADMFEFKVBT@FHH@FEDS@KFGFAHCFLTRDFFIHOHEF@JBPAJCF@LBFAFEJGF@JCFOL@FADKDEFE@CBC\\@XAHEDAJBHKJQBCTCHAREH@FDFJCHDOJAHDDLBLHDJBJDHONEL@HADOBEDIAAJSVAFBFABUDKJEPABE@CACFEBCACBCFaR@AAGGEKGM@EAEIGAI@MFOCKBMEOCM@KDKF@BDLAHA@@GC@SHEHCBOAGGAGKGG@MJCLBHDD@DABE@@BBBFBDABEB@B@BD@BCBCFEBEDDLNF@FCLEDMBICKGGBGDQVMTIFKBGDE@EAGCCEDCJ@LCBEAEIAIGCBCHCDE@KECCEOQMMMKAEGUAGGAICCMCACBELO@GCEGAGDIJKDKBCDAFDN@LEHIDIBeKGAQHGLUPIRAJFLPJdNJJBLATBhBFFJNHJBP@DBDD@LJLJJNFVHL@FF@DAPDVEDG@C@@JEDAF@H@DHFJ@@DEHBFRJPFBDAFFJDDJBDJLJFJHFBDADEB@DFBHAFBDHAHAHDTBDJDAFDJJHHFDJHDFNH@HFCHFBH@B@ZzPDLPDBAPHF@DCHFJCFDJBJAN@NHLCJBJHPPCNAJBF@NEROJK@ECGDCTMFCJJHBFAFUDCHEHDAFDFJ@JF@DJAEHBBH@DBFGDDHAJRP@NFJFhhTNNRJDJLPlRbJ\\DNBXDPNRLZDBFADAJBLCDGJC@GBCD@BHDBFEFDB@FIHDDGBOHBNBD@DCBIIECEBIBABHBBN@BA@_FA@AMEEKGACDCBCEE@CC@CDCAAIBOCAD@HCDGBE@CEAKBKAE@AKGAC@CFCBGCCUE@CAA@CCC@IEEDEFCRCBIBAJCDDDBHEAC@GCEAADKECECFSECCGLMNWFKFIHATA^JZJTRNHJDXBXFHFHJBJMb@JBHJHBTBFDD`FpVHEB@TDJDHLDBPCHFF@DABAGEHKECA[BC@KDEAMHEHUF@FAGODGFETEHGH@@EEO@CNA@GBEDAAKHADCGS@EBECEBAF@BC@ADIJ@TEBAAEDEFAJBBACYGQ@EJKLKFIBKFEF@@CGGIAECAIBGVYJCL@JMCOBEJCBCEYAGNW@CEG@EDCHUDEGGHoLWJCL@DAB@ECEEUGI@CDAJKRCBACGBMMCBCGCAAACI@@ABGAC@@FC@@DBBHFGFAFJJ@BKPABADe@ADBHADIJCNGHKBGDCJIJ@DFBBBWNCAAE@CDGISFUFO@IAEMKCICCFGCCAGCEKFUBIDGLGH@JGJBBJ@ZBFD@BUDEFEBIDC@@CDQGBAFGBKFKBICUOEEFEMKIDMBGA@AFEJEBA@GEGJIBK@MDCJGFM@ECOGQFEJGBCK_PaAIBODSAQHK@UBADANBFCPAVMAMHCDE@KDMDEDABAFG"],
                "encodeOffsets": [
                    [109017, 29523]
                ]
            },
            "properties": {
                "cp": [106.253156, 29.283387],
                "name": "江津区",
                "childNum": 1
            }
        }, {
            "id": "500117",
            "type": "Feature",
            "geometry": {
                "type": "MultiPolygon",
                "coordinates": [
                    ["@@D@XGHAH@LFF@BEBGBKDCJABABGBGB@FDB@DAJIB@BAB@FE@AAC@AF@@DBBFADDB@BAAACCFCBCHBHRRDBC@GDG@CAEAKE@BCH@BADB@@DADBBEH@BAACC@ABAAHGBBDIFBADBBJ@CDBBF@BCHABBBABEAE@ABAF@B@CCBCJ@DDBJEDEJGDBFCFBBPADMBCECBAF@@FBBLGFFB@FCAEBADBDFFBJE@ACAA@HABCCC@ABBDDD@DCFDGQBEHFFACCDGAEA@C@@C@AL@@AEELEGEAC@CD@FDBAACBICCBAAGAAKGCA@CFCL@@OHAEML@PCLDB@BACCBC@AQUJGJBDCBKDALCHBZRF@LMFKAGC@BCKEAC@CDCN@BE@IBAHF@BCHBBHH@BGJ@DDBH@BBD@DCJ@BADA@CAC@IDAFAFB@DGFADHF@JBDBDIJEBCH@BJ@FABEB@HBHC@GAI@KJBBB@JJDHC@DFBIX@FBB\\LEJEDBDCFBBL@HCLEDGBALBJEJABDDHAH@DFBFCDBDHFFPEBBDAL@@@AFJDDDVEDDBA@CBAFABB@BEHGH@DJBFAFEFCHMFARDFNADDBX@DNCDK@CDBHADKACFEBCFLALHB@@AHBPCF@BBAHEJEFADFFFBD@LKDBBJQPJLFAP@@AJACCI@CIHCF@DHBBREJ@DDDJBBBCFEDGDCFAJFD@JE@IBCJDJAD@@D@BFDJBHACH@DJDB@BGRDFCJCBVDFNJFBFAXQFCXADCBEB@ACBCFCJAFBBADBP@@CD@AAD@@CD@@AFABEFA@QHBBABECC@CDAFDFAFSJO@CUACAAEMCAEWA@CCAACDCBBB@BCND@GBAD@BFDA@AAAFCEGD@DCDDFC@AEACEGC@CFCEAAAJMD@BFFABKEAAAAEBCD@DDNFF@FA@AOUFCRADCBIIC@E@@F@HFNFDCCKFGF@HFHABA@CIIFKCM@QAM@CD@AE@GI[IMMQEAU@[KIGEGIEoeCEBGLGBA@IEE[QQMCE@QEK@MOaGYUsAASPOHQiC@MFGDCIEGEAGGCKACEAM@EAACCAFCAICCE@BAF@ACBACIIAEEAEDGAECEWIAHV`IDACIMIACDDFCBCAADJJBDC@E@ABFDFBBBJV@BC@@BDFDABDHX@BKBK@AAFC@AOaIECAIBAHABE@ECAA@CBAH@@KDEIKBCA@EAQFIDEHSLEFCJALDFALGJGDOGQKOGEEEC[O_UQGACAEIEc[mkIPHVDDD@BDADCDCBADDDHDJIBBDJALFTADC@AAOOC@CBADDJCFC@I@ADE@EDAB@BCBGFBBA@CBADGBHH@DIBMCCBBFABE@ACE@CD@NGFC@@EEBCAAFCDB@ABIDCC@GK@EEKCGFC@CKMAE@IEA@ONG@CDIJIDA@@CHCBA@EGGEAI@@BBDCFLNBFADC@EGI@GFAJIHC@EAEBLHBHAJFFHFAL@DDHFDKN@JDDHD@FYFQBiCKBuIIBGBC@CAGIEAC@CJBJHD`HFD@BCFeRSNI@WEGBONKXENAJ@JJP@DIN@JFJLJADGJ@DNFFFDFDBBDILADDFFLCFMB@DSLEHAH@FHHNBBF@FGFCFOLCHCF@DBDFDB@BABEJEJEHAD@JFFHLHNPDDHBBB@DKNKFGH@TIAGJAF[DFH@NDHTRVFJFDJ@HGHOFCDOEUDAFHLABO@CBGTCPBBFCHADJVRMJAFBDFLABCBAD@FBBNHBLCLOVAHLDBADAHGFAHBDDBJ"],
                    ["@@EFDFDEAE"],
                    ["@@@A@B"],
                    ["@@AABB"],
                    ["@@@A@B"]
                ],
                "encodeOffsets": [
                    [
                        [108545, 31106]
                    ],
                    [
                        [108811, 30929]
                    ],
                    [
                        [108715, 30640]
                    ],
                    [
                        [108790, 30630]
                    ],
                    [
                        [108795, 30627]
                    ]
                ]
            },
            "properties": {
                "cp": [106.265554, 29.990993],
                "name": "合川区",
                "childNum": 5
            }
        }, {
            "id": "500118",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@AICUEEAMEICKAIDWEgBGACGO@GDA@G@EEGBKIQ@GACCI@E@IDCCEKACCDCFBBA@IBAHCBA@CCC@E@QKUW]BE@QEOCC@EISAIDIGK@MBMAICIDEEIDGAEECDOEAKOOCYyA@G@EADGGEG@EMGCCIGEIGCIBEICACCSBG@KEEIBGCJGACGEEIKICIIACCEIBEACWIIGACFG@CG@EACC@IBGFCBIJ@FCCUBOAGEAI@UGMEIIIK@KCCCAO@IAMGEIAEAgBSAKIIcMOIEKBILSMEQBGAOIEAEBCFEJBHFDLCD@JRKBCBGNGHIRSDEHIBILGB@BFPCFFF@NEHC@BDOJ@DCD@DE@BD@DG@ABDBABBDE@ABC@DHF@CFHDBDFDCDHP@BBBBDB@@FCFIBKFCLAHAFUVAF@HFXFJdN@HCFEBEBCAKKG@CBCD@LDLABCBBJADBBGLITEDK@E@EBCF@FFH@ZCLFJJXCBC@EFF^KVQhCVBJDHJBbDBDCFUHGAICKIGEGK@OSAEBGBSRUD@FORENGDDBFFGBAD@D@BF@BFBBLCH@FTFDFF@DGJ@DFH@FDBD@JEBAJDPPVDFAFBDDDJJFHBH@B@@FAB@FFAFBHEHF@BFDBFCDFDPADCJEAAF@AEFB@EF@AADABAFFDCBFLBBFD@FBDHHZEL@NLDFND@HLHBJLH@DDDJ@DABFDDTDH@HDHALJH@DDJDVFL@HHRBJLRFJLJPFFDHFF@PEL@DDFFGJBHHJLFDJJNBAL@BACCBAJAJCAC@AB@BCGK@GDEFCFIVA\\JDDAD@DABBBPCBGBABBFHB@FEDIACCBAADEDABBABDBHAL@PLB@BGL@@AGADELL@FPABCEIFGAC@EBAH@DCPEHBFLDB\\DP@`HNAH@HFRTTB"],
                "encodeOffsets": [
                    [108636, 30256]
                ]
            },
            "properties": {
                "cp": [105.894714, 29.348748],
                "name": "永川区",
                "childNum": 1
            }
        }, {
            "id": "500119",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@PBNHNADAFOBAF@BABICKBGFCDBDALGHCH@FDD@FCLCBAACD@VFBMHEJ@PDN@FD@HDBPGVCDE`KNI@AMUGQEEGEAA@CDAFDF@FEJIFEGG@GPAPFHAFAFEJ@HEGICKGEACMOE@AE@QCGDMACDQCIIMFI@WFAB]LMDAFICEBCGK@OBIFADCJBF@FIDABEGMAKEEAICC@CIQABE@ICOCEBEFIDQBEAEG@CNa@YBGFATFHQBSAICCUIECACBE@CG@CAOSAAHMBMDCFAdDJADCJKDGBSR[NGF@^BDGBKACMSMCECEEEQEECGBE@GDCCIFGCGI@EDCAUeEG@CDCBGKGEMECEBMAKEE@CHEBYKC@KDICEEBGFCRGBC@GCAMEGKMKQE]AUI]COGOEGGIODMAKAEGEG@]NUHENFNELEFET@TDJFJBHPB@JFBABE@MAOF[@DRTjEHSJBBHBIVEAECIMADDJABIGOEG@QFEACIKACDAF@JABDWBCAAAGYCCGCOAIGIAKDADFPFBNBHNG@KRECEDG @BBGLCB @FCD @HEJCBCDBFLEDABBDF @HDJCLGJOHC @GGEAK @EFEDELG @EP @FDFCHBDFFNBBBBJHB @JIROFCFCJSTQJEACBIJ @FIFBD @LBBNFKPDBABOFAADECCGFEEC @@BDFGDAFE @CCEBADAHDDABC @CJEEAE @EEO @IA @C @@AHGIEC @@BDBEDEAAEIJC @CCIBCJADABEDGA @ICBAD @FDLCD @BHFALHD @DFD @HHJGHHBBDBF @DEPEHGFKBGFIJIPDJCLFHHRNFHFEL @FJDGLBDA @GAGBGBAFBP @BDBFCD @JFAFJBBAAE @EFADOHCFHLCVBDAFBBFLBBFH @@CB @BDP @BBABEAE @ABAHCCABED @BDFNDNZDC @GBA @PLFEBIEC @UZ @HBHAD @JAHAJGDEAE @AJABC @GGA @@XCVEBOBADATFFGNDXCJ @FDJADABBPAHD¸@HB @JGFABBAJFF @^ DR@TEPBJ @NBJHH @NHTDRBDPAFCJABEBAJBPAH @JEVGJKCGBAJ @DBHNAJDFNDDA @AEGAG @KBGBAFDNBHAFECC @CDWCEOMACD]FGEC@CF @@GDIBAFB @AGGIA @C @CFELIDB @HDDBA @EDCTDDCAC @AEC @CE @CEBECABAJCHCNAFGN @FKDATCPDP @\\HBDEPERCEEJ@BNJDACCDCDDD @PCB @BFLHFBLCHDDF @BE @@BBJADABIBEFBBPDDDDFLCD @HF @F @BD @JEFUDADAB @FDFBDABCD @BBEDDBB @NCJFFCF @DEAAEACQBAF @HFL @BAAEKSACBMBCFCIIAADAbBREN @FFCTBD"],
                "encodeOffsets": [
                    [109816, 30085]
                ]
            },
            "properties": {
                "cp": [107.098153, 29.156646],
                "name": "南川区",
                "childNum": 1
            }
        }, {
            "id": "500120",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@ACBC@SREDDBFFAIWE_BCDADJD@@CEODCLBDEACCAKDAACCAGDIEC@CHUACCAKYMQCOAWGUWuMeIOKEMQSMggIEMEO@IQGBCCEHCAG@AAFGIB@CIEI@CEBEGCGFCDEVEBGAIIEDSNCDDH@FILQPMFE@IAMBODBD@FDDHTCVX\\LV@R@FDD@DABGDAB@JABEACDDDLBDFCD@J@FDJBD@HJRALFH@F@HCB@HHPBDAHFhC\\FRFJBNFFDVBJBBCDAHHJF@DDR`HbFHBJJRAHEFJVHVHDHRLJNPNHLBJPH@BB@FEDLPBDADIP@DM@ABBNDFDLFFBLAHFNBPDL@BGDIJ@DLDDDJHLBNTRLbLhZxxDHPR\\ZvnJFBFHGBENKBCBEAEFIJBDA@CACIMBAF@DEHBFA@AKUFALHBCECAGAKBGACHGFAD@FFF@LCTBoyIE@EKMICEDCDEBCCEGGEAC@KGMBCIMCECSOsCCE@ADCAAEBECAEBCHAB@QECBC@K@QBAHBBOFGEOGAAC@ODENCDEECKBG@IKDCHCFABAIIGSCAC@ECM_AMESHICAGBCQAmDM"],
                "encodeOffsets": [
                    [108804, 30242]
                ]
            },
            "properties": {
                "cp": [106.231126, 29.593581],
                "name": "璧山区",
                "childNum": 1
            }
        }, {
            "id": "500151",
            "type": "Feature",
            "geometry": {
                "type": "MultiPolygon",
                "coordinates": [
                    ["@@@MHQJSPQJAXFJ@TM`MHEBC@AEC_GGCAIDID@FBHJDBD@HAJAvJLAjDRAZE@EGCCC@ILMECCG@CBKGEEEBIAGKGFAFBD@JGBIHEJ@FHD@BCAEKMDEAC@AJ@FBHH@FABGD@DB@JCJIDCH@PMB@FDDAADB@F@NBDLD@HELDFFL@BJBBD@HEA@DCBEDBFA@FD@HE@MDCF@BDF@BAAEDANDJA@CGEBAAADA@AFBLKDA@AFEH@BCJ@D@DECIBCDAD@RRD@BAEUBKCIAAIJGCCCBCDADCBCACC@CCGUJOOQCGssKKeU[GQKMSKAIGCCKC@CJIHC@ACKAOEMBGAKEECKCEAMBAN@@CJOBCACKOFC@EAAG@IOKAMGMOKIGQGCGUIUFEBGIQAIEGE[KWIQG@GIBGDC@@EAOAQSGEG@MB_GO@[CCCCGGCQFCDC@E@@DBHEHFJADOB@EKKCFHB@BK@AHA@OKK@GBCABAAACBCFBBDABDCJEFA@EGAAABAHODAABA@CBCCC[IUBEJEDCF@HHLADA@@BBDIDIBABDDABK@ABNLFBDBHJH@JJHDDHFBBBX^FBABDLLRDBD@D@fdTfFPB@HCPKD@BBLZBHLTPTLXLPLLUPFLAFCDFHABKDCCEAEFAGCAK@KD@BDHABEBBH@JBDADCB@JCDC@BDEB@BABEAEFC@GBE@CCEEA@WDKEIBEGEDABGDGHIAAFGDGAEBECQACJJJGFEJEAEBCCBC@GIILIGKAGAAUAAEDCGICGEEUBOAiBMFABDLGJQEE@CBALDN@FID@CCACD@FMHE@@HGBADA@CDBFAFCDEBDDLH@DDFHJHBJFJADBBD@JBHV\\@FJ^BRH@NGF@DDALPTF@DANDB@DCFGL@JNBADACMBCF@DHHAXKH@BACA@AB@F@HADCAC@OFCFAP@JFFHABAFEBHJBFJJDBH@HDBNHPCFIFMXIDCF@BJADDHHBDIREACDFNEDM@@BBFCFGBKEC@CB@FBFADGB@LBFLRFFADI@MDEHSHGFOBQCC@ABAHBBHTHJ@BIHABFRAHBBDBHABDDADF@@F@BCD@DEDBBDFCF@@JFH@DMJAD@DPDDNABGB@BCDFFDHFBADDFDBBADDLBBCFCB@@BBBHALEBOHEHCDEACKEDIVCF@BB@LLHAJJBHAJEBA@IFE@EJA"],
                    ["@@@CABBB"],
                    ["@@FABAEABBCB@B"]
                ],
                "encodeOffsets": [
                    [
                        [108525, 30783]
                    ],
                    [
                        [108714, 30641]
                    ],
                    [
                        [108789, 30631]
                    ]
                ]
            },
            "properties": {
                "cp": [106.054948, 29.839944],
                "name": "铜梁区",
                "childNum": 3
            }
        }, {
            "id": "500152",
            "type": "Feature",
            "geometry": {
                "type": "MultiPolygon",
                "coordinates": [
                    ["@@BGPUDKAKMGAA@EBCDABAEKACBENIUQCIGBGDBKBEHSDAP@BAGKBEVCPFDCPEHK@ECGIEUESQCG@MEG\\CBEHIJB@SHGLELM@CAAGACCMOKGEGIEC@GBIFIFAFABA@ECAC@CDEDGPKDEHE@EAEMAGG@EFKFGPG@CJ@DADGIOBCJKACCAEIKCEEBCHI@AKIEI@IJM@CIQIB@FEF@JABIFGBIABIKG@KAAE@UDCJLFBDCFKFCDAP@@KFGBAA@ACBED@BKACCCBEGBCEACGEEDC@AHABCCKOC@CBCNI@CEG@IE@EDACCACFC@ADE@@@CECBACEBEA@IEQBAJG@AGIIUBGBAD@RDPAHETGFGHAPC@CCCKSAOJCAG@GH@JFD@FABC@IN@FCEMDCFBFIDIIICCIB@ADEJCNWJEDEGOAMGCG@IEMUFADGGGECKAG@GDAJ@HBDCDGBG@@BDBABG@WLGBCGE@ADDNCBABIMK@EHCDA@MCCBE@OSBKCCE@MHG@AQI]@EU[AG@ICEA@IBSIEGCE@COIDECIBCACEBEGCAEGC@[NAAC@CDGGIASIACI@@CIDC@CDDJLPFLJFHHF@BD@DCBAJIJC@ABHPJFBAB@DD@ZGDADF@DDABEDADBDBDAFBJADDDGHDBD@BBDDKFGAQ@ABC@ABCFEDADBDF@DDALE@BDUVBDHFDADBFB@HDBFD@DCBBDADHHADJBHJDBCTABCLCBABBRHFBF@HF@@DAA@BBFJ@BD@BDADFDADBAAB@D@@DBDADCFJ@BB@HDDEDBDOJFFBFADIAQMG@CDAJAHGJC@MKKAA@GFAJA@GGIDGE@JGTA@EBEDBRELEBCAKGGAIFEJEBKCEBAF@TCNJJ@FGBIAGDBBEDC@ECE@IHKBCBIAG@MLINEDAHBBCDAAMB@DGDBDDBPBBBDFTFDHDPADE@EFGAEDC@EEEDI@EGEACEICAIACA@QF@BFF@FABAECACBECIDBFABDBANDFJLHPB@@CH@TJJAJEBIDAJAFBFDJLJDPIJALFHJD@H@NCF@RDDHBJAPDHDDF@NCD@BDDDCFMPA@ICKAKDMLAFGHKD@JFDCDBJCHOB@FTLBB@NBHFDH@FG\\BB@JIJ@DADEF@DBDBBCDQBCDAHBN@LDB@FBNDFCDBDAD@BADDFCACBAF@DB@CHFAFCJ@BDDD@JIDAF@@DEDCFJNBDADG@EBGCC@EDAH@BECE@ADBBDDAB@BCN@BFB@BADGBCB@BFDHAF@BJABGFCHFNHBFAFADC@CHAHHJCF@AFEHGN@FHDXDJFDJGP@JBDLFVXCHOH@DLFE@@BDDBHADDBFJL@NAFDLLHPAHCDABBDFDBBLC`RDBLCLM@CDCACBIFABCRI@AEAACEM@EFCHEH@FDAH@DVNAFBBHGPDCEECAC@@L@@AACCC@GDGDCDAFBDFHDBDCDQDBHJBDGFABD@JDBFANBBCBEB@BJDBDICIE@GCAEHKDBDFJBNJB@LIBKTENE@AECBEBAF@ZDXJDAB@AG@CPB"],
                    ["@@HMCECBCJDH"],
                    ["@@CCB@DBBABGEBG@GBBJNA"]
                ],
                "encodeOffsets": [
                    [
                        [108522, 31103]
                    ],
                    [
                        [108091, 30921]
                    ],
                    [
                        [108115, 30853]
                    ]
                ]
            },
            "properties": {
                "cp": [105.841818, 30.189554],
                "name": "潼南区",
                "childNum": 3
            }
        }, {
            "id": "500153",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@PGBGCECKBEFAHK@EBAPEEINCDCGSAcJMHSFGBEB@HB@FD@@AB@HFBAAEJ@BFFAFCBCAIACDKDCF@FBFDBDAB@DJHFLFBHBN@FBHAFENDD@@CECBEFABABECEHAHG@AACFC@AI@AG@CFE@CMECACEBCHK@AI]AAGBAAAOJ@BDFBDAFEBCCECGBALBNFNAJEDC@CAMBCFEF@FCBAAEAIJ@FIBFFDCJB@JCDBAHIBCDBLCD@DF@CHBDBDDCDEBIDEFDTDFDBAACBCACCAEFA@EKCGLG@CCIDC@AAC@ABGFEBADAAHDDD@B@BIDAIC@AFAHCPGL@BAAEHADD@FBFPDNLJB@DDBTSBC@AC@@CJEJ@BCFK@EMICMACBED@BDBABE@GAAEC@GCEIG@CFEH@F@JJDABAKIGMME@@DCAEMKGFEAEB@EBA@EA@G@GAIECICCEAEBUCOOICABIFC@CA@EEG@CHI@CEEECESG@KDAAAEE@@A@CBCHAEECAHCFMPQ@EVCTOCE@EBCLKBCAGGIQASDGDEFCFJT@FKJEBGBEEQKK[ACIA@FBNBPCBCBMAEBYIG@QJG@SHCGBAECODKAUEKIIAKFEH@HJNADGDE@cOE@UDBDCDMHOBA@BDLDFF@BCB@HAD@DDBLBBFABI@C@@DBLAFQNE@COEEIDO@QECCIIAD@TFJCNALEL@HBFfZDD@HKJAHLH@BBBDAFD@DEJBFBB@DUPERCFSJEAAGCACB@BCBC@EBMDoEBDCBB@ABHF\\J@DCJBBD@LCFBCFBDIBHDE@BDC@ADCBADDB@BA@@BAA@BAAA@E@@CA@AFA@AA@BCB@BAAGBOAEAAC@SAAGACBEJEBGAAGCCKAC@OHCFBDNFHDBF@LKPKBIFAJ@HHVEXCJU\\CBEDDFUDIfBDDABDDBCJFLBJXLLAFDALEHEJBFPJBH@HBBB@NK@EIE@CD@LBBCAEFABBAD@DLPEFABLBBLFLNLNFHABADIHCHBHDBJELAPDDPFB@JFBBCHCNGFABBDCBBDH@AHDB@BXIJHJLHFNHHAFIPIPALCNDRCJDNAFDFD"],
                "encodeOffsets": [
                    [108015, 30392]
                ]
            },
            "properties": {
                "cp": [105.594061, 29.403627],
                "name": "荣昌区",
                "childNum": 1
            }
        }, {
            "id": "500228",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@GG@KACugC@ECOCMQOEGIA@KRGBEACICAACDGCIIICGIMCO@ICAOHADGFEDEAEBOPKBCCGBWPHNEJDNCHDBALFDBD@DADIB]CGFAHGJC@EGC@IFEACAEBECKFC@KG@A@CFCBECKGIEGEC@EC@BGEAFEEAHC@GBGAOBEIC@CBA@AEGBCCGDABIDCDCACEEAFM@GLKFAFBB@DED@BC@CBSPEABDAB@FD@AD@FGB@BCAA@BDGBBDADAA@AA@EDK@IFGHGBEGMUIIEIMIISQWWSS[KQQQKOECIFAH@F\\`HPBHAFEHQLYBWAEFHH@H@DGFI@GIOGKIA@ADFHCDaIK@cDGDIRKBEDDPAFADQIUDCA@GGIMCI@MNMAGBQIGIGKEA[FIA@BBFEFGB@BDDEAADCA@DGHUJURK@EDWFHLDAHEHCFBJFPHNNFHABIDQPCFBBNDJLVRDHPD@HBBHDDD@LNJHBNLDHDLFHDN^bNNDH@DABMFEFMJUHWBIL@JEDCDM@AB@@ADC@JJ@HADDBBBC@EAADBDBBAJ@BKDa`AHGHAJGFCFBJBDHAP@B@BHLFCDE@AD@FBDFDFLAPDBBCDABEDAJBHEBFFDAFCDADCD@DB@JANB@JHB@BALDBHBNHFCDBAJE@E@ABHDNCDB@DMHHJK@@B@FFJAHCBEAMJB@D@^RTDDHFDD@FALKDGBAACFCD@DB@HF@@DDDRCFDFABBDCB@FBFFFCD@BCFDDAD@BBD@FGFGD@BFDBPBJALERCDEAKBGNIHCFCF@RDJFFFHPZZJNJHbfH@HCHADBHPLNlEFC@GDCFCD@DCBQDDB@FEDBDCEG@E@ACACCAKBED@DEEC@CFBBACC@CDAEEDAFABADE@CBGD@FCHBFANHL@JELAHCXYHEJARDNGVCFDLNXRLFPKHITYHQHC@CCG@GJKDCPENIDEJIF@FGTQFITQFClCfIPATBPD`PFFT@VBLCZOKKCKGG@EFC@IACHIACFC@AEEDE@CCISGQ@MASFGAYSCEAOAIOCBIPuJIHK@EGACA@CFOHG"],
                "encodeOffsets": [
                    [110628, 31308]
                ]
            },
            "properties": {
                "cp": [107.800034, 30.672168],
                "name": "梁平县",
                "childNum": 1
            }
        }, {
            "id": "500229",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@FGHCDMPGHAJMFCh@PDZANDJBTATIFCDG@KDEAMBKJELGJAPULCNInAHAJENCVE\\U@EDELMDADGFCHKHCTEBGDAXKLCPFH@V@VBJCXOH@JFJBVIJDH@ZONCN@PMTGJAFDB@heFAPCVKNKHILILGPIRSHKHCL@JCXK\\KHCHIh]@IBEDCJ@BEAGMYBKBCDAd@fQHBHBNERIFB`EDCASHSFCKCYECIMACCBIDA`EDC@ECCDCNCFC@GE]DOCAEBSCOFO@MBIDKBGDEDA@AADK@GI[BAHGDGAEEEADWFED[BEDMCE@CDEAWHC@EACCAEA@A@ADGDCJ[HKAQK_BCAACIAE@CFMAGDO@IBIFEBCGG@CACGMCCECAUBcAK@GACAACFOHE@CCCGAACAEDMAEKEIA[@iDEB@DBDDD@BKCAACGCAKCi@CDCLQBGAAA@C@CEAO@EBOPKBOLGTEBU@CB@FABC@ECQBEAeD[C]JCKAAO@GEBEGKKKEQ@QMIKAACECCGQBIIUCEFOHC@EACEBGGASDEGGASDG@CBCBIAEGGBIFCD@DBPCFCFIDMNEEM@EFEAUJK@AFG@GLGAEBADDDEDSFA@CGA@WJMAWJED@HKD@DKHADBFHDJBBDCRGT@HGHSHADpfLFBDM@EHKBGAGFKD]PA@CC@FADBHUEEBIDK@GBGDELBDTXBF@FGHGBKAOCCE_DGHkTQJCF@REJEFDH@@@B@BMDGAECC@EB[DUJOTANIDEDCHKPUZGHIDK@ABACC@IHCFHdAHITAHGJGBMLKFBDJ@DF@DCNDHF@NIFAD@DB@LDDJDHAF@TRD@JEFGRKXE\\UNPPJHF@B@DILEB@DIDKDYNIPEBABDDRDJJJBLDJHFF@TELOHIBEDCHBFFFFBFCF@JBHDNJFBJCNELKFAHDJ@LB@BF@NKLCJAHDVN@B@BH@DFDLAJBFAFSDEDONEHCJED@DBBADEHKJCF@JKNQNM@@FCBK@GD@BDABBFAFF@DMJWT@DBBL@FBPPXRHDH@dQDBHHH@HCJALGRGRBTAJDFDJNFFRBRLBALCJGFCFCPIHGHGRCPBLEhCDCFGHMBAHAJEH@JGJCFGRIJKFCTE"],
                "encodeOffsets": [
                    [111254, 32901]
                ]
            },
            "properties": {
                "cp": [108.6649, 31.946293],
                "name": "城口县",
                "childNum": 1
            }
        }, {
            "id": "500230",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@FCB@ACBCJILBDALOLI@CCIECGMFEHCAELK@AAAHUEGECQWA@GFEHKCCBQS@CTMfgT]BE@IBEDAFAPBFE@GCEA@KDIEHGFIDANBHCJADGHCDGHEBG@E[[JGTIVS\\DB@BHJBHHF@JDLBZCVGDEAUDCF@TLPDF@NSCIBCFCDDPC@IFC@AEAECAGCC@CVYRIHBFANMECAEECCGECCBECGKCGOSCBQLCAEE@IDKDCFBDAEMGKI[NEFG@EK[KEGAE@EACEBMECOECE@GDIHIFCFAJ@PFDANDDAFoGICCEAIBEA@ADE@CEEAGIEOC_Jc@EGGAWDOFILKFADBFABCAGEKAWPEB[@KCIBGCE@WHKLGEGBIAWBEAMIEIGECIIEKIEHADDX@JGXQ\\BHCFUA_NEBGAE@yNYAGAGGIECGB]DC@EAOCQGQBGAAKQCM@GDCAE@CDEHE@ECUCCKCAA@EDICCC@CBELCDA@GECBIDGRUEABHNaLSJABHHDAACHCFBBFGFG@CFERMBGJC@IEIKUOEAWRGDMT@FADIDEJEDFDZjFPFJ@BABE@eHOJGBWEEH@PCDEBSR[BEJKFEF@FFXALHHDJBVAFIFELGBADHBFHFBFLFCFBEBEPAbBHBF@^AFICQOGCI@IDKHAL@BBF@^CDMBCDFFLFDTRHBBADFLVRaFILMJHRELBLEHAFCDE@AACFEBMHMRBFBDHB@BBHENI@IEICCDE@ADEBABABBFKHDFAFIDG@CFCAAAKLAFEDCCIMASUGA@GHADBHBB@DEFCBAAMGA@@D@DEDMRML@BH@FFL@AHFHJFBD@P`dHLdlDDNFDBBLAFKHGDADADPLLHbNBJATBDJDNJHAJCBJAJF@BDKHMBCD@BHD@FADAFAVKL@DN@DDRLNBRFHFFF@DOVJAHBHL@HBDH@JGFAJDFFHHBPFLLF@FAJBDRDDDFPAFBJBBHBHGLAJG@ACEASICCCFKAEHENBHFPPT@DBDDFBDABCAAGGBC@AHBBADCBIAAGACBCHGA@GJE^DBBCFBHAB@DBBHDHADKFGF@FBDFHXBBTFL@FAJE@CCGBMCCIAEECOBGHEBGFCXABACABAD@FEPAFGLBF@FEFBDGDA@CFAXKXENKBAGMUOEKSMCIGQ@CDGHEZED@TVD@FCNA@EFBHAHIBIRGLFJNTTDBROBGBGHEBA@EVW\\eXCRKRIRAXKNDHH"],
                "encodeOffsets": [
                    [110591, 30776]
                ]
            },
            "properties": {
                "cp": [107.73248, 29.866424],
                "name": "丰都县",
                "childNum": 1
            }
        }, {
            "id": "500231",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@CGSSQEECICCAO[SOMMIECKIG@MAAKMIC[_@CDCbIRGDC@C@ECIBK@AGEEAIBE@CC@CLYACCAE@OFMM@CBCFEFIBMAIEAIBGAG@ACACEEBKAEEAIBAC@IDCJEDEIIAUFAJDL@LDFDDH@DEPCB@BVFDIDCBGB@BCCEDEACL@HAPOFIEIGESGK@QKCCM@@CLKBUBEBC@EGC@ADCNALGACE@BIAIIDGBMIICACBSAIaMKGOKBCBCHCLGBEAKCAMECCckGK_c@OACIEEGBGK@EEG@BFGDAHGBKAEBCDALRP@BSNEHWNC@EMGEE@KQOKQOquECA@GHMFQBK@IEOGKCGBWVEBCJAD[L@HFFHBR@BD@DMPFDJDG^EFYLM@ECKBFHAB[N@D@JABULGFCHLBLABB@DDDNBBFAF@DDJD@VED@JNGTBHJHFNAPCFEJC@[CC@CFAJ@DNPHLLNFJ@HCDIBIFONADGJE@CDB@@DAFILSLMJMDEHJPJJD@bxJXJdP^HFFPDRHN\\bRNNHPN^dBPDFDJ@HCNHH@J@HLPBF@JHNHRJJHNJJDJJFDJNJNXNRDJXEFCL@VQVIHG@CDBBCFBCC@AHAFEAE@AJB\\EFBHLHJRJHANBNMJ@NDHJ@HDBVCRJBCBECOFCLAJQHCdCL@bJDCEGBCB@LJPHHJJ@HE@C@GGGFEXBZARKFGBEAGGO[_@EBGJE"],
                "encodeOffsets": [
                    [110246, 31151]
                ]
            },
            "properties": {
                "cp": [107.348692, 30.330012],
                "name": "垫江县",
                "childNum": 1
            }
        }, {
            "id": "500232",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@PMHSJGDC@CC@IQEEE@KBCC@MBERSAMACcaqICFWCGBKE[DEAIIBAAHEECFC@EACBCICCGGAFCBA@AEBCEBGDC@CC@CEC@@CG@AEGC@UFU@MGSQUEI@GLSBSDMLQRMFG@IAIIWAEF]EW@CFERQDEBKAMGKC@CBAJABCBEAEGGYUMIC@CEMACBAIEKOGCGEQIGDGJM@GFKDQ@KGEDCA@EJWGKASEIAQCC@QFYCGOMCE@QJU@EIIMAEACEAKHQACOGYWICKAKDGCMAEEG@I@ADULIDSBEDB\\DRBZVpDVFNL`EHE@GBOHOTEHODKVCBMDAFBDBFAJADEFIJEAMOIGYIAE@CHCQgCAQAC@EGAG@GCANAD@JCNADaEGHGAKDO@GEGBEDE@KKBCD@@A@GEIE@CBMBCDCNJN@FC@@CC@EDCFAF@TGBAHAVBPMNDFABGFO@GCEEAICGGEKEOMCAGBS@SS]QGCM@CBIGEFOFABFH@F@DCFDLSFOJE@E@[[E@GDE@ECQAGGAHEB@XEJJNDJCRBDCNDH@RBFF@NPBDHFDLHJGFI@EFEBGBOEOB@HHHEFIJEFE@ECCB@DBBHFFFHRLRBFSLYJCFUDMHC@AGEEM@OCE@EBCBCPUEC@BDABKDEDKCMDKHCBCAGDDTAJABE@ABGRMBOGQAGbMRED@DFH@DED@BFDNFBFJDFERDNF@BCB@DFFFBF@NFPLjbFH@LLPBPZZFJ@HDDFBH@TFT@VPJCD@LDFFHDHFJA@EPEJGDGPCDEAC@CF@FBDAASJSAEDATFAJDBDBDEHAJJPHAFBDHBJDH@DQFIb]PSPMJOJ[PWDIF@LLJF@JFHDLFDNH\\@BF@FDHFDZ`FBF@HAPATF@DCVBDADAD@NADCFGBGDDHCNBJBHHDFA\\HXLRLBDDP\\ZFJJHFJFHBDHDBJH@FFJFDFBFCDFDDEJCBC@ENSHCXQNHNJJLJFD@HINAHUBAH@DAFCEGC@GDBDCBGGBATIbKGMH@PDHQJCDAHFB@DCDGDED@DBBDCH@FBBLDDDDV@FGFCF@DBFCD@HDNLRBBAHHRDRBP@FCDA^DHJFJJ`BLC`EJCX@`MVBDEAGR[HW@ICWBCFGLJJFDJHFFJNJFBXAJBHAHFLKXGF@HDJALD\\@FAXO"],
                "encodeOffsets": [
                    [110662, 30325]
                ]
            },
            "properties": {
                "cp": [107.75655, 29.32376],
                "name": "武隆县",
                "childNum": 1
            }
        }, {
            "id": "500233",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@FDLPRRLRT\\XTRXJTNJFJJJNVFHHAHGJEL@FCB@@BBBBCACHAACB@DB@AHA@EBCC@@EBAACFBTODAD@@AFC@CAABELEHKN@BEFFBDCDCDAJCBDHADFH@BAB@DJDAFBPAH@HGDFBEFFBAHD@@FFDFHHJDLAFED@D@BLHD@LEFDFADBFBJED@FHD@HIBGHE^DJABC@CACECBKCADGCMFIGMXOHADDLAPOFAFBFCHEBCPGDB@JDPJNDHJJDJCHBDDBDJBBH@DALQB@HJPFNRPDFDD@vhBD@LDFJFJ@NENANFFHJFJBPEBA@GDCD@JDD@HCF@DB@BCFBH@LHJJBDGLBPGAQAAK@FMBEBCDAL@NHJCLaFG@SB@D@PJDBF@FC@IKU@CJGBGDEDAJLHDTEFEV]piBEAYBIFGJIDESWKKEKUUICkJG@KE]UQIGOOQGUEGEEGAI@cBMCOEKKACBIPBJ@NGHIFADCFEH@PHfBPEVOAEKKGEBGOKBG]MGI@CAABEPIJA@MAEGEGEI@CEBMDMGMC@CD@TIHC@QGGAEDIJEILWDANGEIAGBAHEH@LFJJF@FC@AMOCEBCDAD@LBBAGMGEC@MJKDEKWYGKEGKCM@ACCYECIACCDCHALBFC^iDKAIOWcsU[IEC@UAGBCDADHXCHEBS[IEIAE]EBSOMCWLQBQJQLUDCBYdUX@FABGFAHAHQPCASSIMKEQHAJGJGBEA@FMBEDC@SUC@YFGFCH@DHRDJTNFLVPHNABMLWFWLEB@DCBCHEAEFE@KAEHOBEFC@ABDBABWBEDAHGFAHDPFFJBDDANDH@DIFEBK@SEAAGWCEEAE@EHCLGBGCAA@CBAAGDEAA]CIF@HHBDGDAHBBBAJCDABGA@BADHHBBADCBEACCCAS@OOGEMAGFBFELDDJDBTDF@BIHKBEHE@ECAIBEEOCCQCACBI@EKEEKAOMMICEBIHG@AC@GEIICGBMFM@BDCFDFADA@AHCDCJUE@ADAFO@CCGECKCK@ICEBBVJJCFIFCD@JBDJAFBBFALFFBDBDH@HBJAFBBJANEJEFAD@DNNPEF@DBBDKZ@DDDF@JAFBHF@BALDJ@JCDEDmNCD@F\\^JDLNBB@NJHDLJFNNTPP\\DBJDFDRFTTDH"],
                "encodeOffsets": [
                    [110246, 31151]
                ]
            },
            "properties": {
                "cp": [108.037518, 30.291537],
                "name": "忠县",
                "childNum": 1
            }
        }, {
            "id": "500235",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@@QBAF@@CAGEIAOBIHIAIAEKI@IIEEUKMDCJ@FE@ENARGJEHGJ_CEBGBCJIFKHGDEBEBQHIBgACC@OBK@CCEOEEE@GD[LCKDaNQD]CKBEHCb@HCJGTEL@BA@ECKBMGMEC@CFK@MGa@CDCL@TEP@PGJ@ZDTPPDHDHCRDL@FCJ@DCDGFQFC`AFHDBFBHON@FCHSFEH@BC@CAEOUGOMMECCGCMOKIWBCLEDQ@SA_AKCEE@QJEDAJIDoH]DIAACAC@SFGLGBCBGBMCCIHE@KEEE@EFIF_@A[DADGAAAJU@CECcGmCKAEBAB@NADEB[EKCIGCK@KIE@AJC@ACGEACICC@ANI@EGEBAFCBCI@@CCCAEBE@IFABCCG@AFEBACIDEAGLG@EBCHEAGEG@C@AIE@GEAAAAKHICEECCBCEKAIEC@SGBG@GFAFBHABGKIECECAIBCHCDKAEKIAABCHCDIACMUAEBCLKOMAKGCI@IJCASOCACDEHEBKCIESQMMMMqYSOCDMLMFqhHHCFJ@BDAFFFDABHLB@FPDCHBBHB@FFFARBBBLABABAZELGFE@CFAACAA@AL@FA@IAAABACCCBCCGAABCAA@EA@CKBAEG@ACC@OCC@AAAAABCAGBIZABEBeI]IGACBCDDHALZVCFDHEBaAMAEB@DHN@BCBEJLJBHCFCBG@GCC@IJCHALCLAJCDERBDLHBDMHEVDLAHKLABY@BDCDC@AGUEEAABEJBD@FCHQREDDLADMHEFIBCRGNADKFBJGVCFCBACYCGGDCACOIC@ABEXDLAXFHCD@DJJILGF@LADKHE@AAY]OKMCWEU@E@CBCFCNITDH@FIHCFCLAJCBMLAHGJ@FPDDF@BAH@DBDLF@DABEBIA[IKDI@EECE@CFI@ECCMCIBGFECWEM@CAEGGBCD@VCHBHDF@FCFEDKAIHA@E@EEMAW@BHFH\\NJBRAJ@`NH@HDDJHVAJDJGL@BVH@BAHBDJDHDLBTH@BCFDDHBH@RFVA@DCDCF@DBFBBF@\\IFAD@ADILINAFDFRDHFFXADADFPHFD@JDPHLHFJRHBDENCFMDKBCBBBADGCCF@BVLDTCNFFD\\H\\DFFBHFZZBJBDD@PCNPJBBF@RKHADBBdJFBBF@LCDKPUVEHALFPJDXBFBLAJB\\@bFBDBFIR@JJFFLFABDDBABCB[DCHDJJHVHN@\\EF@DBBDBDIN@DJBFD@PDJLHTJDJBJRATETIH@RKL@DABE@EMQMIAC@CFETIRCJE@CJKLE@ACCQC@ADGCODEPAFABAAIFCT@PCTAJG\\CDCDBJDBD@DEZBHBFBHCFMFDB@LHJFBNFF@DEPAFBDHTCRJJMPCFBF@NDH@D@@CFEHDLFDBVKHA\\CLCH@TKPA\\C"],
                "encodeOffsets": [
                    [111669, 32120]
                ]
            },
            "properties": {
                "cp": [108.697698, 30.930529],
                "name": "云阳县",
                "childNum": 1
            }
        }, {
            "id": "500236",
            "type": "Feature",
            "geometry": {
                "type": "MultiPolygon",
                "coordinates": [
                    ["@@BGLICIMLGNLB"],
                    ["@@JGLWDEHCFAJFPBT@HFD@NALEFI@CGMGECG@KDE@KLCRKLIJ@ZNL@D@HEfSVEXFXAVLNBREN@FADELWCMBK@EWY@EN[@CAAI@C@EDA@GECMEEMCMIG@EG@GCGCWECEGCAKCaQMBGAaUI@SKGE]IGEgOMM@AJET[AECIOKCEAODMACGOFIDICICC@CFEDK@ELGDGBEBCACBGQ@ODA@AGS@KAI@SKEAHGBIDEDQNCR@FKHAXBJGHBDAAMDMOCACBEAEBADABELEFGJAHIPBL[DEDABCAA@ECA@ELS@KLQFOCC@ABA@KFOACBGEMQYoaoGWIESIUAEDCHABEOOGABE@EGEKAIBqQIB@DPVANELQAGGSIMKECWSMUCEeGOGQWYMEKOgOWq{[Y_kSSE@GJIFETBRADEH@DPZ`d@HSZEJMNc\\OHAF@BRFJXCDGAMKCAW^@DBJGAii]OYQcGKGAC@CBANFPDHABASM]MqGDIAY@ECGMYSCIAAEAA@EHAREBMGE@EBADDJWTSdDJF@GFGLDBNI@B@JEBAF@tNDJP@HDDFH@BCJKPEDG@ECAGKCCBELCBGBIAIEABKGCBABK@GBCAEDGDEFAHEFCACIECQCIGIHCFBHNVBDCJGDADBBLJBFCLGDADBHFFFDLJAHGBEAEB@HAHTHD@JFLBDFDAFDDFGJBLBBFB@HJF@B@DFHBHGFAD@FMJDFCFDJGHDJADEB@JAFBFDD@DJ@ADEDABHFBDCFKF@DDBDJFBDH@BGBABBDHDAFFNFHHFNDVDFABC@MBAFALBnDdHFD@DIVBBHBBC\\C@BE`EJ@FFFLFF@JGDDANAHADKHEH@TBDBDJB^CpGJCBIFCRIF@DFBLB`@TCRKFADJXBDLFFPDHFDNNHPPVDHCFG@EFCHAJCDCBM@GPEACAEG_BEDERCHCDI@EDK@QCGDGCOCSOYCI@OHO@SFK@CD@DHb@NEL@DFDHNANDL@FABK@SFIHGDa@GDAFDLC^MRCbDL\\KHCF@FFFPDDL@PAD@BDAhGJARAFCFGHELIJADAHDFI`GHIFQHMB@FEFI@CDLNFVJF@JLJBFBJGJAJBPFJBH@DE@AB@RLCR@NFNFNEN@NEJAHBNHLDbGTBLEfAHCJAZ@FBF@VGF@VHHALGB@NJFBlELCBCHBHEPCF@FBLCH@HAHCDBRABEFkPaFEPGNADADEAEIABCDClAFAAQBCRIHO@AAGBG@CCEFGFCHAR@THJEXGFMB@NHJDBBBPHDVFd@VCFI`AD@DHDFZHJ@LE\\@FAFKBANB"],
                    ["@@CB@AKAETILCHDLHDHFHBRB@sEA@GCA"]
                ],
                "encodeOffsets": [
                    [
                        [111718, 31314]
                    ],
                    [
                        [112199, 31995]
                    ],
                    [
                        [111720, 31314]
                    ]
                ]
            },
            "properties": {
                "cp": [109.465774, 31.019967],
                "name": "奉节县",
                "childNum": 3
            }
        }, {
            "id": "500237",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@AK@MCACACEAEBSDMHKEGFIHBJCHMEWCCACDEACBGHGDIBKCMJgHMNKLEDGACIKIECE@ODE@IGGCAEEUCCCICOFOCAABKACUCAAEKEGKAEIFICGBODEFCAGD@FDDCBEACEA@CECCIBOACGA@I@CR@VA@CCAACBEFG@[GKBOCIPBhHFED@HCBMIMKCAEDG@EGIKEAC@ICE@KGKCKBGFGBGDCIUEIMOEMIMIOCE@CCKAGECEE@CDAGECEMGAEGCECAEDGCACEEC@AAABA@CECGSC@CBGDICK@UAIBIJADBHIJBJO@GDAFGDEDABBHAJBNCRCBEJIHEJ@JFP@BABIBICEJMAKAKDKEMEEECCI@EBI@CBANCHA@GCGAKJQBMFIHIBMAKGSMmQw_KCEEmGaOk_gYSIECOKCEAEBMACACDGHEAECCQEMKA@ADBFDHBDADCBEAMGEKGGIAGCcGEPKR@LKT@FDB@FBBADCBCFK\\OAGJIBEHKFAFCBABBFAFBDPDCNBNCBGAIHWAGBELQ@MDCRCFAJGHFBTLJ@LBT@BHB@PCR@AHBDADAFCHKH@FCLEF@DDDDJCJEJHPBDCNBPDFPLDJBFS\\IF@BNNhPHF^JHFTLJ@bVHBNAbRLDDBFHFDDXDH@HFHH@NJNDFFDNHFB@FCD@J@BB@DM\\@FXZ@FALDNKXCFEBM@QFMAUKWBWEUFeTGFC@K@YMI@KJQLKD@LCF@LDHHFHN@DEJKFMBC@GES@OAIEEBGDCFKXIHHJNLPLL@TD@BGBAD@BNZDD@DJDD@@BCNJBTBDB@DPHFB@HDDDBB@NKFID@LDAKDCH@NDd@JJFBHAPCH@BBAD@PETBLJJCL@DDFDLDFRLLCH@DAPBJCJGLATNBPF@NA@HHFAHBLL@TDHDLBFDHFPABDNA`IhL`HZB`AJDFFFLPXFVFFAFCTFJBFGTJbFKJAPBTJ^BJ@JEHEJMDQJIZMTCJCBC@CCC@ATQLGDIBABCDA^AVDPCVFNGFBJFD@DC@CAEBGJKHC\\EFEBKGQAGFONKEMBETWBGACCAEGAIDCLADABQBC"],
                "encodeOffsets": [
                    [112808, 32055]
                ]
            },
            "properties": {
                "cp": [109.878928, 31.074843],
                "name": "巫山县",
                "childNum": 1
            }
        }, {
            "id": "500238",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@NEHEJCJIVEL@NJNDH@JFJ@RGJAXBPFDAPCDM@CBATADCBCBCJBFC\\BDABC@AKQFEBWRSDIEMBK@UEMJEPSFGHELCDEAAOCMAGCA[FG@CWEQICMKM@CRMNCJ@PBPBHALDTA\\DFAJOH@lZRBP@VEVQDCDMFEFCRALEHGNEX@dDJAHAJMJEDEF@VDR@RJFBFA@AGCAAGGWKCKCEOCOKAC@CDGIcHQAEEIDSBEEEEUOWEKCCKE_B]C[EgK_JMBACOBGEECKAGCSCK@AKBGGE@GMBE@AOSMKBIHIDOACBG@KDQKCECKCE@CDKIIAKFS@OBCAAG@ODGBEAIIc@MCG@CDBLKCC@EJMLC@EE@GEAOG@CCASAIADM@AC@IC@CCCMY@ABCHA@ASCK@OKMKGIMAABELEB[@KFI@YGCECGC@_BEJUDc@UEGCAOAAICMGA@ENWHGFC@MEGAQBIDEHDF@DAHBH@BGPQJADBREBkBCDADJBBFCFCBMBOHEFObElADQDCAGDGBG@KDEAE@ODGFGAADGBKDgDOKA@KHGBUGE@UHE@EAY@IBGDeBKFSAaHKCMGGAIBMFM@MFMEKEQAMF[DOBSLG@KD[DGBULCAQICBCD@DI@OCE@I@KBINQISDCGOAEBCFE@MEEAGI@KCANEBCBCESFU@GACCAICCD[DIHSBODI@MBABBJABEBOBCFDPCH@BRDDD@BKFIL@DIFQDSJEF@DBDNJNR@FAFCBK@QLG@SJSFQBOJYCAB@DCHEFGBEFSFG@MJE@A@FF@DQT@HFL@FAFGH@D@DB@FBHDFCF@HAL@HDJCHCDD^H\\CFDBFDJBTBNP\\DDLBBFNBDBBNHRDNDDBBRCN@FD@DB@\\GF@HDLBJDJJJRbXF@FCHBHDDHAJGHGBAD@FDFHHATJLCJFFFBAFBDJBRADKDCh@NDDBDHBBLD@ACCAC@CFAzCVBLHARBFBDHBDD@DGFEPBDFDvBVADBDFNDDHDBH@DHFAJEJAP@HCNBDEF@JBBDDB`ARLLB\\GDIHCBCB@B@BFDDFBD@XGFBDCF@NDFC\\AFCXEBCFFBFCHGHABJ\\@HCLDBFCTEJCNAP@PETDFAJBVKNACfAPBpGLBLDdBDD@JDFFDD@HANDNAHDVBDCFK\\ADB@HDBN@NBZIDBDDPBVHLAXHNFFBD@PGJAH@PFLC"],
                "encodeOffsets": [
                    [112216, 32489]
                ]
            },
            "properties": {
                "cp": [109.628912, 31.3966],
                "name": "巫溪县",
                "childNum": 1
            }
        }, {
            "id": "500240",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@C@AEGC@ECAAGDEAIDAF@BACOBEDIACEAGECGAEDERMPYAAKCAA@SFEAEKE]AGCDK@M@CDADBTMPELOBEFAhDFDJDJO@AHG@E\\ODIJEDEFABGFE@ALED@FALDRK@CFEC@ACFEECBCJ@BCFC@EBC@GDEDAH@HE@EAC@CNITAHOL@RBB@BEGQIOIAEI@CFEKaQCUTKJ@BAMYKcQmU]EI@UECCAADCBCAMQEMBEBAJDHHF@JG@EKE@A@AHADCEe@iBAH@JCHIAQCEECACCWKQQcCOAOReAGECFQCGEECAACDE@EBI@IEICFGJBDCHQBEACCIQWAUEKBG@EDCJgRK@EC@KACE@AEGEEB@ACAACCAAEEB@CA@AACBGDIJIC@CJI@AUOSIACFIECAICE@EACDAHA`ADAHBCI@GIACICGBGGIISKUCBECEKAKBEFABA@KCGF@@IBKBMHOBGAAOJMJM@UKUGE@WXOBILAFCDGKAAIDO@GFM@AD@FGBWJIHCHCAGGCAOBGBGG@GGKFGDIAECEGC@AFGCKGFA@CCCGECCCAMHEBGCCCA@ICCGAEI@ECAO@QMK@EAEMDGOEI@EBEDGJCJBJBDPFFDANDFFBF@HBLFL\\@FEHMFJ\\HLFNCBEACDCL@JFFDBRKDAPTDHHLFDDAFDDHFDBFDD@BKLEBGAQJUZ@DDDBHFDFB@BED@JODCCEDADDJMTE@OCSKE@CDBVCFUHYDKAICE@GGIAAGA@[CWVWLCD\\^@DAHGFCHGDCHIBGDMACBEJGHJFLCB@DF@HEFOAEBCBAF@JAFS^ehQLADJPHFDALDFGHEB@RXFDFHGVBB@BKLBFGDEFHNFDDJ@DKJKPCBKAIJADBDA@EDLHFAF^JBJFT\\FADGGWBCDCHAVBD@JFV\\dtPXBJCL]jEDKAGBCDDDJBFDDZBDN@LDFHHLXZFLLCNID@HFHNABKAC@CBADDFNP@BEDE@IIKEG@GFABBHFJMHCBKXDJFCFEFCHBRHF@HG@SDCD@HNCNANDFJ@HFHFBF@NIBOJAFBB@DHJ^NAHPLAF@DHDNPADSNOFeAOGG@EFCDEBGJMHI@OAAJBDLLPFNDdAJ@HBFFFHHVPRHPRJ^VLFH@dIJ@LDRVTKDCBGBAXAHADEHSFKDDX`DBH@XKH@BDFNPNHNJFBH@H`bBTHTLNN\\JNnJFNCJBDDFJJJFADCBC@OAEKCEECEBGBEHCJBD@BKAGBGFBHHRHB@CGEGEM@GBADG"],
                "encodeOffsets": [
                    [111027, 31221]
                ]
            },
            "properties": {
                "cp": [108.112448, 29.99853],
                "name": "石柱土家族自治县",
                "childNum": 1
            }
        }, {
            "id": "500241",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@AWFEE@ACLIFKC[DICEAGFI@KFG@KBKIOAOAGBIHEHAB@FDHBJA@ACCBA@GGBAABCLCNADCF@FC@CF@@EACDAX@JCFG@IAESMAEDEHC@EC@CGKCAAKI@CI@AEBCACG@EBOGGG@GFGDAHAHCJK@EIGICiSkOWMIEI@GAEIAIQKAABCDEL@@AEE@AFCXMBGB@HJLHJBHFJFHHDDH@FCLGJIFEN@VLRB@HRVH@FCFBBADBFEB@AAD@BCACHG@MEACIDCFAFI@CFIF@FA@CAABEAAEDECO@IEAIGEACASCAGU]M@KBOFEDABAEGKEBIBIAABOE@QKSI]IIGG@CHKBKDGAGEKACGOC@OACEEAGCCKACABACGJKACAEEGOBECDIEUC@MFC@AAAG@CJM@A@CCAAEBQCE@GOcEEGCVKFEBCCEQKGIFS@GKOIOGOKKGC@AFICIIIICIGCGCAEAOAAGKQJO@EAI@AVUHMDSAEAEGCMAEUGAQJGFILGDM@MFGBCD@NEJICEBGHC@ACBEYc@IFM@IAEGUGGGCU@GACBIPEFIFAAMDQLYHODMFCFBLBHFDBDCJQJE@QC]HGAQIeMSMG@IBSNEH@NMNCL@TEJMEO@CACGIYeMGCEEAMM]EAABABCH@PDNCRCHGFIBECE@C@ABArADKDEH@JCNAD@DBLBDFDHBNRNJRFPFJNBFCFKFADFNHHBBDFJFFV@JCPBFJNBJ@BCDQBSHEFDH@FNVBBBJDHFFFDBFFFE@KVEBSJ]NADDDDB@LIFAD@BI@IDGBEHCHCVBTHTBLDHCJNAF@FJPJHBDD@@HJBHHDNBJATHBBCFBB@PPVHFAFFDBFNFPNBHFHPBBBBDF@PALJPDDDJANJHDBBTDPGBCDAF@LFPDHDF@\\IP@bMBBDFFTBFBJDDFRHRFPJR@FDJ@FBFFBLEFBJXDNFBDBHIJALGPPJLJJDFJJHRR`Lb@HCLHHNFDHNDBHH@HFDDDBDDJTNHAFDJ@DCBC@GIICCACB@DFNPHDHB@JCF@FJFDHAZFDD@HXLJBHAPJDDHCLBNPJDBDRPBLNFF@FFFCl@"],
                "encodeOffsets": [
                    [111858, 29574]
                ]
            },
            "properties": {
                "cp": [108.996043, 28.444772],
                "name": "秀山土家族苗族自治县",
                "childNum": 1
            }
        }, {
            "id": "500242",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@LBHCFALMRETYXIFGDGLIAEDKGSHMBEKYBCHA@EGYIODIHAD@dITSDEGMMQCKCCEA@GKQBKGCKSLK@A@CPODE@CEEbkD@PFJ@FHDDJFPEFILALMPGRSZMDEDEVWDI@KAAG@ACDEDOJK@QBABAP@PBJAPIDED@DBFAdQFANDJCHADCDUFCFM@GCEKIM[MEGEBIEM@_K[CCEAGBKAGIK[AKEMCOAE@EDO@UG@ECCGAGEGGAAABEECCI@Ck@EDEEE@MEAKQOACICMOKAGDCCOIGBIAWK@GCCYEGBECEIE@IDA@CGOGEM@CDADBJDHJD@DA@CCIBEMGISCCCACCGEG@AGMCCGMEGGDK@GKaQ_GQIICEIIIKOOKHIBGJCAEACMIWEAKFEAAE@ECI@EIQEOGQEQCCAIAEESCEAAaNO@[JE@GCOCKEE@CBADOHSCAAGCMIIBCCOCKIOBE@ACAAOAEGAGOMMEAEECBEGEOU@OAADEAASGIBMAGCAGGI@@CCGAOIEIE@MBDICGAKGSAM[TOeIAOBKGKAKEE@SFSOGCGU@CVKZKDCBMDC@CD@JGN@DEFCFO@GLI@IEKCAKAIIG@CBC@CEGAEC@KKQCM@KBGBC\\JDADCBE@EQUAAQAYEGBCDC@GCGGEAOCO@CB@JEJGJKDaBCFAZCHBDFDADADATBTPZDDTBP@FBBBD\\EHDR@DMFCFCNADALGFGBEFFVCJKNIP@DCDBFHFDHGDAH@REN@DHBADLFNDlBBDBDCDERAlE\\FJBBVXb\\BBCHBPAFCFQD]OI@CBGHCDEBOCADDLAFSTKFE@CAEUCCYACBGFGBMKM@EK_I@EDEACICGBKFE@IAGCK@IBOLEBE@OEG@IDkVcJQDKFUP[NEFKHOEMBGDAFNRHRJ\\CZBZPHDNNJHLJFFNBDDBLBBBFXBREREHSFQDIFKBADBLBZM\\CPBHBDNRBLAJFXO\\@p@LELMLKFQDEBADCLC\\EPE^NFLABBRZJLJFHJNHj`JBNBNJHBL@FEVBFFJDDLHFHJHBRKHDJHHXFHF@NEH@HD@HLHJBZ@HBFDNDBHHHHTAHDNFDFDBDXFZDDCHCFEDGDIB@FDH@BAAC@GBAF@BCJGB@BDF@DIDC@ACCKEEE@EFERKDFFFFL@HGDBDHFR@@ACEAIBIFKFCFDDCE[FAH@NJTHXIHIFBDDBHCJENBBLBBDADIP@FFBVEHBJFLAFCDEDMDEfUHCF@HHFDHBLAFDDFABEBEFFBHADB@HCNBD^ARBBBDFANBFFDFCF@RDBDHDN@DALMP@ZG@AAG@CBAFDBCACBARCHBBBJ`EBDLAHJHNbLPVd\\|FDbJTNCJDLJDHNHFDHHJBHFFBLDBH@AJG@IDK@MFYNADAJCFMBIB@FHLBRHHJPDDLDF@RANDTBfAHFPBDFBJCRBFDDN@JRLAJEL@JBL@H@DCDBAfBHJPLFBDHBDJBD"],
                "encodeOffsets": [
                    [111671, 30109]
                ]
            },
            "properties": {
                "cp": [108.767201, 28.839828],
                "name": "酉阳土家族苗族自治县",
                "childNum": 1
            }
        }, {
            "id": "500243",
            "type": "Feature",
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@NKVEDABC@EAA@C@A@EEIEAGEUAECAGDABEAECGG@AC@CAE@EKQEcFCFBJALCDEAGDCBAHHFAFM@KZSFADCBSAMDCLEDKHGAK@GAAMAECBEAA]JG@KCIEAEGSGEEIAIBCFEAIDGDAHFDBbOVCJCHBXCL@BBdQHEDG@CEEAE@EBEHGDMHEBEAEMUO[ECOAUECAAkCCEEAGHE@EAC@EFCDELQBGGWGGIQAM@EBGJGF@FIH@JAJEBEVaCGGECGKKAE@EJS@CGGGQFKAECEECGEKACAEGAI@EJYBIDEVYNSIWEEE@ACAIEC_CQEACECECAGAEBGGSGGAGMCECGAY@IAKG@GGCG@MFE@EGGWIGGCQLGAGIGECKICEEUAEFK@GAMIMAMEe[MGGIIEIKOYCAKBMEABCJ@RBH@DERIPGZ@LPnBDADGHKDSHQDM@CCCEBEBC@GKMEK@]EQECOIACAEAAWAEDG@APHBFLAFKHIDGFG@G@C@KRCJADG@UEAEM@EA]OGEeCCDCRCFGFGDMDKHEBMACFiJKDICCCCCAGOMCEC@GE@CECAEC@EE@EAIGACEIGGAIBKEIACBQC@UDO@IEGQMSMMGAI@YDGAAALMCE@KEGGCI@ECG_AQAFEBKBMAOD@HC@GBIFGHSHOHFPHFBLHLAFBH@VBRDFHDJBHDHAHAHBJDZXPHBDGR@JDDHFNBJJ@FIV@RDFPNDHEZ@RDDBRFJBTHLIX@FDBFCLHR@LCHEN@HIHCRJHFHDLPJFABBDFN@DJDVNHZFHFBDABABIDAD@HLBNALCFQREF@DFXE^BFJXBJ@JEHQNKRCNATKT@HFJRVHT@NEV@VHDBFH@@DD@DFD@@DCDAHDFFA@BABEDHBDHJDADBD@FEDFDGFBBJABJCFF\\ALDHEXJDrdbBDBNQTAF@NDDLAF@FFJRD@@DCDIHGTONLBHFDBBAAEBCLEJKPERCNBFHd@`IPDHDDJFDBBAFEDHDJAFBHFDLCNA^CBMCEBAHFNFBNBPLP@DB@FFJHBDDAHHFBDAFGFBNDDFDDHDDB@HEDLEH@BHDDFBFCJEHHL@HHHHAPADBHHDBDGJGXIHA@EBCN@HEP@JCBBHLDCBEJKPAXWF@VHVLN@NIPIBB@BIVANAL@JE@DH@JADCBABALHPFDDALVJTHJAHHRJB@HFPBVEJDDL@HE@GAABABIDEH@JEJBFGBUCEECAMEIDOFKHA^LFADCBM@IGKEABMESTCJ@TD@EDELMFA`EXYHEF@"],
                "encodeOffsets": [
                    [111111, 30422]
                ]
            },
            "properties": {
                "cp": [108.166551, 29.293856],
                "name": "彭水苗族土家族自治县",
                "childNum": 1
            }
        }, {
            "id": "500154",
            "type": "Feature",
            "properties": {
                "name": "开州区"
            },
            "geometry": {
                "type": "Polygon",
                "coordinates": ["@@@BCCTJJHHDLBBD@HPDPEDILGHGFMLIBKLOFABD@LFBHADFFFBATLJBFJLIFEBANJB@FIACB@LCHEBAEGO@CAJUBK@I@CLCFGBGAODALAVJBHCFFBBADIFGJBFA\\@JFDGH@HEL@JGH@LHTLDLJDXVJBL@D@BFGHABHXFH@BEFBDJHTHB@EKDEVQH@HHJJLBhTFHHJXL^JVNRDHJTHDCD@@DEHWLM^ETNAJCJAFHLDRJXRHNXR\\FBB@HHH\\NJBRAJ@`NH@HDL`AJDJGL@BVH@@AH@FRFNDTFADCFFBFBJ@RHTC@DCDCFBF@DDDF@`KD@@BUZAFFFPDJFFZEFFPJFB@ZLNJFJPHDBKVKBKBEBBDABECCHABVJFTEPFFF\\F\\DFFBHD\\\\@JDDB@PENRLB@F@RKH@DBBjL@F@LCDIPUTGJAJFRLDVBF@L@J@\\BbFFHKTBJJFFLDABDDBABCB[BCJDJLHTHN@\\GF@FDBFIP@DJ@HFANDL`RFJBJQHYCAB@FCHEFG@CFUFG@KJGBDF@DOT@FDL@HADGJ@HD@FBHDDCHAH@J@JDPGDD^H\\CFDHPBbR\\BDNB@DNDFB@NJRBNHFRELBHDADD@\\GF@HBJDLDHJLR`XF@HCHBHDBHAJGHG@AFBFBFHHATJJCLFFM@GBMNKDOLITC@U@CDAD@DC@EEQBG@eDYC]JCKCCM@IEDCGKMKCS@QMGKCACGAAGQ@KGUCEFOHA@ECECDGGASBEEIAQDG@IDGCEEG@IFEFDRINIBMNCCO@EDCAWLK@@FI@ELICCDCDHDGDSDGEYHM@SFµyIUPY^QI¡£BGJu[Uă@aBO~m_±£HS\\QTaEK]Oa__ee[QNqNEtHHGb@JO@g]]EM[QF__C[o]KKOfmD_]EO³mUyWQHQSB£\\[vYU["],
                "encodeOffsets": [
                    [110563, 31635]
                ]
            }
        }],
        "UTF8Encoding": true
    });

    
    //var hjDatas = [
    //    [{
    //        name: 'PM2.5-①',
    //        value: 5
    //    }],
    //    [{
    //        name: '温度-①',
    //        value: 1
    //    }],
    //    [{
    //        name: '湿度-①',
    //        value: 3
    //    }],
    //    [{
    //        name: 'PM2.5-②',
    //        value: 4
    //    }],
    //    //[{
    //    //    name: '川东',
    //    //    value: 1
    //    //}],
    //    [{
    //        name: '温度-②',
    //        value: 0
    //    }],
    //    //[{
    //    //    name: '北碚',
    //    //    value: 2
    //    //}],
    //    [{
    //        name: '湿度-②',
    //        value: 0
    //    }]
    //];

    //var convertData = function (data) {
    //    var res = [];
    //    for (var i = 0; i < data.length; i++) {
    //        var dataItem = data[i];
    //        var fromCoord = geoCoordMap[dataItem[0].name];
    //        var toCoord = [106.5, 31.5];
    //        if (fromCoord && toCoord) {
    //            res.push([{
    //                coord: fromCoord,
    //                value: dataItem[0].value
    //            }, {
    //                coord: toCoord
    //            }]);
    //        }
    //    }
    //    return res;
    //};

    //var series = [];
    //[
    //    ['采集终端', hjDatas]
    //].forEach(function (item) {
    //    console.log(item)
    //    series.push({
    //        type: 'lines',
    //        zlevel: 2,
    //        effect: {
    //            show: true,
    //            period: 4, //箭头指向速度，值越小速度越快
    //            trailLength: 0.02, //特效尾迹长度[0,1]值越大，尾迹越长重
    //            symbol: 'arrow', //箭头图标
    //            symbolSize: 12, //图标大小
    //        },
    //        lineStyle: {
    //            normal: {
    //                width: 1, //尾迹线条宽度
    //                opacity: 1, //尾迹线条透明度
    //                curveness: .1 //尾迹线条曲直度
    //            }
    //        },
    //        data: convertData(item[1])
    //    }, {
    //        type: 'effectScatter',
    //        coordinateSystem: 'geo',
    //        zlevel: 2,
    //        rippleEffect: { //涟漪特效
    //            period: 4, //动画时间，值越小速度越快
    //            brushType: 'stroke', //波纹绘制方式 stroke, fill
    //            scale: 8 //波纹圆环最大限制，值越大波纹越大
    //        },
    //        label: {
    //            normal: {
    //                show: true,
    //                position: 'right', //显示位置
    //                offset: [5, 0], //偏移设置
    //                formatter: function (params) { //圆环显示文字
    //                    return params.data.name;
    //                },
    //                fontSize: 12
    //            },
    //            emphasis: {
    //                show: true
    //            }
    //        },
    //        symbol: 'circle',
    //        symbolSize: function (val) {
    //            return 2 + val[2] * 1; //圆环大小
    //        },
    //        itemStyle: {
    //            normal: {
    //                show: false,
    //                color: '#f00'
    //            }
    //        },
    //        data: item[1].map(function (dataItem) {
    //            return {
    //                name: dataItem[0].name,
    //                value: geoCoordMap[dataItem[0].name].concat([dataItem[0].value])
    //            };
    //        }),
    //    },
    //        被攻击点
    //        {
    //            type: 'effectScatter',
    //            coordinateSystem: 'geo',
    //            zlevel: 2,
    //            rippleEffect: {
    //                period: 4,
    //                brushType: 'stroke',
    //                scale: 8
    //            },
    //            label: {
    //                normal: {
    //                    show: true,
    //                    position: 'right', //显示位置
    //                    offset: [5, 0], //偏移设置
    //                    formatter: function (params) { //圆环显示文字
    //                        return params.data.name;
    //                    },
    //                    fontSize: 12
    //                },
    //                emphasis: {
    //                    show: true
    //                }
    //            },
    //            symbol: 'circle',
    //            symbolSize: function (val) {
    //                return 1 + val[2] * 1; //圆环大小
    //            },
    //            itemStyle: {
    //                normal: {
    //                    show: false,
    //                    color: '#0f0'
    //                }
    //            },
    //            data: [{
    //                name: item[0],
    //                value: geoCoordMap[item[0]].concat([10]),
    //            }],
    //        }
    //    );
    //});

    courserate_option = {
        tooltip: {
            trigger: 'item',
            backgroundColor: 'rgba(166, 200, 76, 0.82)',
            borderColor: '#FFFFCC',
            showDelay: 0,
            hideDelay: 0,
            enterable: true,
            transitionDuration: 0,
            extraCssText: 'z-index:100',
            formatter: function (params, ticket, callback) {
                //根据业务自己拓展要显示的内容
                var res = "";
                var name = params.name;
                var value = params.value[params.seriesIndex + 1];
                res = "<span style='color:#fff;'>" + name + "</span><br/>数据：" + value;
                return res;
            }
        },
        backgroundColor: "",
        visualMap: { //图例值控制
            min: 10,
            max: 85,
            calculable: false,
            show: false,
            color: ['#f44336', '#fc9700', '#ffde00', '#ffde00', '#00eaff'],
            textStyle: {
                color: '#fff'
            }
        },
        geo: {
            map: 'hjkj',
            zoom: 1.3,
            show: false,
            label: {
                emphasis: {
                    show: false
                }
            },
            roam: true, //是否允许缩放
            itemStyle: {
                normal: {
                    color: 'rgba(51, 69, 89, .5)', //地图背景色
                    borderColor: '#516a89', //省市边界线00fcff 516a89
                    borderWidth: 1
                },
                emphasis: {
                    color: 'rgba(37, 43, 61, .5)' //悬浮背景
                }
            }
        },
        series: []


    };





    courserate.setOption(courserate_option);
}
/* =======*/
/* 成本分析模块*/
function initcbechart() {
    professionrate = echarts.init(document.getElementById('professionrate'));
    //        option = {
    //            tooltip: {
    //                trigger: 'item',
    //                formatter: "{a} <br/>{b} : {c} ({d}%)"
    //            },
    //            legend: {
    //                orient: 'vertical',
    //                right: '0',
    //                y: 'middle',
    //                textStyle: {
    //                    color: "#fff"
    //                },
    //                data: ['test1', 'test2', 'test3'],
    //                formatter: function (name) {
    //                    var oa = option.series[0].data;
    //                    var num = oa[0].value + oa[1].value + oa[2].value;
    //                    for (var i = 0; i < option.series[0].data.length; i++) {
    //                        if (name == oa[i].name) {
    //                            return name + ' ' + oa[i].value;
    //                        }
    //                    }
    //                }
    //            },
    //            series: [
    //                {
    //                    name: 'FK',
    //                    type: 'pie',
    //                    radius: '60%',
    //                    label: {
    //                        show: false
    //                    },
    //                    emphasis: {
    //                        label: {
    //                            show: true
    //                        }
    //                    },
    //                    roseType: 'area',
    //                    center: ['35%', '50%'],
    //                    data: [
    //                        { value: 335, name: 'test1' },
    //                        { value: 310, name: 'test2' },
    //                        { value: 234, name: 'test3' }
    //                    ],
    //                    itemStyle: {
    //                        emphasis: {
    //                            shadowBlur: 10,
    //                            shadowOffsetX: 0,
    //                            shadowColor: 'rgba(0, 0, 0, 0.5)'
    //                        }
    //                    },
    //                    itemStyle: {
    //                        normal: {
    //                            label: {
    //                                show: true,
    //                                position: 'inside',
    //                                formatter: '  {b}'
    //                            }
    //                        },
    //                        labelLine: { show: true }
    //                    }
    //                }
    //            ]
    //};

    professionrate_option = {
        color: ["#F86464", "#19DC7C", "#34A6FE", "#FA9022", "#DCDB01", "#8C70F8", "#2A4AD1", "#E76FE3", "#5032C0", "#168FB2"],
        grid: {
            left: '1%',
            top: '1%',
            bottom: 1,
            right: '1%',
            containLabel: true
        },
        tooltip: {
            trigger: 'item',
            textStyle: {
                fontSize: 14
            },
            formatter: "{b} : {c} ({d}%)"
        },
        legend: {
            x: 'center',
            bottom: '5%',
            type: 'scroll',
            textStyle: {
                color: '#ffffff',
                fontSize: 14
            },
            itemWidth: 16,
            itemHeight: 8,
            data: ['石灰', '水泥', '石膏', '废浆', '料浆']
        },
        calculable: true,
        series: [
            {
                type: 'pie',
                radius: ["8%", "10%"],
                center: ['50%', '46%'],
                hoverAnimation: false,
                labelLine: {
                    normal: {
                        show: false
                    },
                    emphasis: {
                        show: false
                    }
                },
                data: [{
                    name: '',
                    value: 0,
                    itemStyle: {
                        normal: {
                            color: "#68c5ff"
                        }
                    }
                }]
            },
            {
                type: 'pie',
                radius: ["53.5%", "54%"],
                center: ['50%', '46%'],
                hoverAnimation: false,
                labelLine: {
                    normal: {
                        show: false,
                    },
                    emphasis: {
                        show: false
                    }
                },
                name: "",
                data: [{
                    name: '',
                    value: 0,
                    itemStyle: {
                        normal: {
                            color: "#0674bf"
                        }
                    }
                }]
            }, {
                stack: 'a',
                type: 'pie',
                radius: ['12%', '45%'],
                center: ['50%', '46%'],
                roseType: 'area',
                zlevel: 10,
                label: {
                    normal: {
                        formatter: '{d}%',
                        borderWidth: 0,
                        borderRadius: 4,
                        padding: [0, -50],
                        height: 40,
                        fontSize: 12,
                        align: 'center',
                        rich: {
                            b: {
                                fontSize: 13,
                                lineHeight: 5,
                                color: '#41B3DC'
                            }
                        }
                    }
                },
                labelLine: {
                    normal: {
                        show: true,
                        length: 20,
                        length2: 55
                    },
                    emphasis: {
                        show: false
                    }
                },
                data: [
                                       
                ]
            },]
    };



    professionrate.setOption(professionrate_option);
}
        /* 比例变化*/
        //var changedetail = echarts.init(document.getElementById('changedetail'));
        //option = {
        //    tooltip: {
        //        trigger: 'axis',
        //        formatter: '{b}</br>{a}: {c}</br>{a1}: {c1}'
        //    },
        //    toolbox: {
        //        show:false,
        //        feature: {
        //            dataView: {show: true, readOnly: false},
        //            magicType: {show: true, type: ['line', 'bar']},
        //            restore: {show: true},
        //            saveAsImage: {show: true}
        //        }
        //    },
        //    legend: {
        //        data:['',''],
        //        show:false
        //    },
        //    grid:{
        //        top:'18%',
        //        right:'5%',
        //        bottom:'8%',
        //        left:'5%',
        //        containLabel: true
        //    },
        //    xAxis: [
        //        {
        //            type: 'category',
        //            data: ['2021年','2022年','2023年','2024年','2025年'],
        //            splitLine:{
        //                show:false,
        //                lineStyle:{
        //                    color: '#3c4452'
        //                }
        //            },
        //            axisTick: {
        //                show: false
        //            },
        //            axisLabel:{
        //                textStyle:{
        //                    color:"#fff"
        //                },
        //                lineStyle:{
        //                    color: '#519cff'
        //                },
        //                alignWithLabel: true,
        //                interval:0
        //            }
        //        }
        //    ],
        //    yAxis: [
        //        {
        //            type: 'value',
        //            name: '嘎嘎',
        //            nameTextStyle:{
        //                color:'#fff'
        //            },
        //            interval: 5,
        //            max:50,
        //            min: 0,
        //            splitLine:{
        //                show:true,
        //                lineStyle:{
        //                    color: '#23303f'
        //                }
        //            },
        //            axisLine: {
        //                show:false,
        //                lineStyle: {
        //                    color: '#115372'
        //                }
        //            },
        //            axisTick: {
        //                show: false
        //            },
        //            axisLabel:{
        //                textStyle:{
        //                    color:"#fff"
        //                },
        //                alignWithLabel: true,
        //                interval:0

        //            }
        //        },
        //        {
        //            min: 0,
        //            max: 2.5,
        //            interval: 0.5,
        //            type: 'value',
        //            name: '哈哈',
        //            nameTextStyle:{
        //                color:'#fff'
        //            },
        //            splitLine:{
        //                show:true,
        //                lineStyle:{
        //                    color: '#23303f'
        //                }
        //            },
        //            axisLine: {
        //                show:false,
        //                lineStyle: {
        //                    color: '#115372'
        //                }
        //            },
        //            axisTick: {
        //                show: false
        //            },
        //            axisLabel:{
        //                textStyle:{
        //                    color:"#fff"
        //                },
        //                alignWithLabel: true,
        //                interval:0

        //            }
        //        }
        //    ],
        //    color:"yellow",
        //    series: [
        //        {
        //            name:'test1',
        //            type:'bar',
        //            data:[2, 4, 7, 23, 25],
        //            boundaryGap: '45%',
        //            barWidth:'40%',

        //            itemStyle: {
        //                normal: {
        //                    color: function(params) {
        //                        var colorList = [
        //                            '#6bc0fb','#7fec9d','#fedd8b','#ffa597','#84e4dd'
        //                        ];
        //                        return colorList[params.dataIndex]
        //                    },label: {
        //                        show: true,
        //                        position: 'top',
        //                        formatter: '{c}'
        //                    }
        //                }
        //            }
        //        },

        //        {
        //            name:'test2',
        //            type:'line',
        //            yAxisIndex: 1,
        //            lineStyle: {
        //                normal: {
        //                    color:'#c39705'
        //                }
        //            },
        //            data:[1.0, 0.5, 0.8, 0.9, 0.6]
        //        }
        //    ]
        //};
        //changedetail.setOption(option);

        /* ===*/
function initechartrandar() {

    juniorservice = echarts.init(document.getElementById('radar'));




    //function getVirtulData(year) {
    //    year = year || '2017';
    //    var date = +echarts.number.parseDate(year + '-01-01');
    //    var end = +echarts.number.parseDate((+year + 1) + '-01-01');
    //    var dayTime = 3600 * 24 * 1000;
    //    var data = [];
    //    for (var time = date; time < end; time += dayTime) {
    //        data.push([
    //            echarts.format.formatTime('yyyy-MM-dd', time),
    //            Math.floor(Math.random() * 10000)
    //        ]);
    //    }
    //    return data;
    //}

    //var data22 = getVirtulData(2020);

    juniorservice_option = {
        backgroundColor: '',
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            top: '10%',
            //containLabel: true
        },
        // title: {
        //     top: 5,
        //     text: '车辆故障分析',
        //     subtext: '数据纯属虚构',
        //     left: 'center',
        //     textStyle: {
        //         color: '#fff'
        //     }
        // },
        tooltip: {
            trigger: 'item'
        },
        legend: {
            top: '250',
            left: '100',
            data: ['产线OEE', '重点关注'],
            textStyle: {
                color: '#fff'
            }
        },
        calendar: [{
            top: 50,
            left: 'center',
            width: 250,
            height: 190,
            orient: 'vertical',
            range: ['2020-09-01', '2020-09-30'],
            splitLine: {
                show: true,
                lineStyle: {
                    //color: '#000',
                    width: 0,//设置外边框为无
                    //type: 'solid'
                }
            },
            yearLabel: {
                show: false,//是否显示年份
                formatter: '{start}年',
                textStyle: {
                    color: ' #fff',
                    fontSize: 18,
                }
            },
            itemStyle: {
                normal: {
                    color: '#040f3c',
                    borderWidth: 0,
                    borderColor: '#040f3c'
                }
            },
            //日期标签	
            dayLabel: {
                nameMap: 'cn',// 中文显示周一周二,
                color: '#fff'//字体颜色
            },
            //月份标签	
            monthLabel: {
                nameMap: 'cn',// 中文显示月份,
                color: '#fff',//字体颜色
                verticalAlign: 'top',
                formatter: '{MM}'

            }

        }],
        series: [
            {
                name: '产线OEE',
                type: 'effectScatter',
                coordinateSystem: 'calendar',
                data: [],
                symbolSize: function (val) {
                    return val[1] * 26;
                },
                itemStyle: {
                    normal: {
                        color: '#f4e925'
                    }
                }
            },

            {
                name: '重点关注',
                type: 'effectScatter',
                coordinateSystem: 'calendar',
                data: [],
                symbolSize: function (val) {
                    return val[1] *26;
                },
                // showEffectOn: 'render',
                // rippleEffect: {
                //     brushType: 'stroke'
                // },
                // hoverAnimation: true,
                itemStyle: {
                    normal: {
                        color: '#d93824',
                        shadowBlur: 6,
                        shadowColor: '#333'
                    }
                },
                zlevel: 1
            }

        ]
    };
    juniorservice.setOption(juniorservice_option);

}


        /* ===*/
        //var edubalance = echarts.init(document.getElementById('edubalance'));
        //option = {
        //    tooltip: {
        //        trigger: 'axis',
        //        formatter: '{b}</br>{a}: {c}</br>{a1}: {c1}</br>{a2}: {c2}</br>{a3}: {c3}'
        //    },
        //    toolbox: {
        //        show:false,
        //        feature: {
        //            dataView: {show: true, readOnly: false},
        //            magicType: {show: true, type: ['line', 'bar']},
        //            restore: {show: true},
        //            saveAsImage: {show: true}
        //        }
        //    },
        //    legend: {
        //        data:['test1','test2','test3','test4','test5'],
        //        right:"15%",
        //        textStyle:{
        //            color:'#fff'
        //        }
        //    },
        //    grid:{
        //        top:'18%',
        //        right:'5%',
        //        bottom:'8%',
        //        left:'5%',
        //        containLabel: true
        //    },
        //    xAxis: [
        //        {
        //            type: 'category',
        //            data: ['工藤新一','工藤新一','工藤新一','工藤新一'],
        //            splitLine:{
        //                show:false,
        //                lineStyle:{
        //                    color: '#3c4452'
        //                }
        //            },
        //            axisTick: {
        //                show: false
        //            },
        //            axisLabel:{
        //                textStyle:{
        //                    color:"#fff"
        //                },
        //                lineStyle:{
        //                    color: '#519cff'
        //                },
        //                alignWithLabel: true,
        //                interval:0,
        //            }
        //        }
        //    ],
        //    yAxis: [
        //        {
        //            type: 'value',

        //            nameTextStyle:{
        //                color:'#fff'
        //            },
        //            interval: 5,
        //            max:50,
        //            min: 0,
        //            splitLine:{
        //                show:true,
        //                lineStyle:{
        //                    color: '#23303f'
        //                }
        //            },
        //            axisLine: {
        //                show:false,
        //                lineStyle: {
        //                    color: '#115372'
        //                }
        //            },
        //            axisTick: {
        //                show: false
        //            },
        //            axisLabel:{
        //                textStyle:{
        //                    color:"#fff"
        //                },
        //                alignWithLabel: true,
        //                interval:0

        //            }
        //        },
        //        {
        //            min: 0,
        //            max: 100,
        //            interval: 20,
        //            type: 'value',
        //            name: '所',
        //            nameTextStyle:{
        //                color:'#fff'
        //            },
        //            splitLine:{
        //                show:true,
        //                lineStyle:{
        //                    color: '#23303f'
        //                }
        //            },
        //            axisLine: {
        //                show:false,
        //                lineStyle: {
        //                    color: '#115372'
        //                }
        //            },
        //            axisTick: {
        //                show: false
        //            },
        //            axisLabel:{
        //                textStyle:{
        //                    color:"#fff"
        //                },
        //                alignWithLabel: true,
        //                interval:0

        //            }
        //        }
        //    ],
        //    color:"yellow",
        //    series: [
        //        {
        //            name:'test1',
        //            type:'bar',
        //            data:[21, 14, 17, 12],
        //            itemStyle: {
        //                normal: {
        //                    color: '#76da91'
        //                    },label: {
        //                        show: true,
        //                        position: 'top',
        //                        formatter: '{c}'
        //                    }
        //                }
        //        },
        //        {
        //            name:'test2',
        //            type:'bar',
        //            data:[12, 14, 17, 23],
        //            itemStyle: {
        //                normal: {
        //                    color: '#f8cb7f'},
        //                label: {
        //                        show: true,
        //                        position: 'top',
        //                        formatter: '{c}'
        //                    }
        //                }
        //        },
        //        {
        //            name:'test3',
        //            type:'bar',
        //            data:[12, 14, 17, 13],
        //            itemStyle: {
        //                normal: {
        //                    color: '#f89588'},
        //                label: {
        //                        show: true,
        //                        position: 'top',
        //                        formatter: '{c}'

        //                }
        //            }
        //        },
        //        {
        //            name:'test4',
        //            type:'bar',
        //            data:[2, 4, 7, 3],
        //            itemStyle: {
        //                normal: {
        //                    color: '#7cd6cf'},
        //                label: {
        //                        show: true,
        //                        position: 'top',
        //                        formatter: '{c}'
        //                    }
        //            }
        //        },
        //        {
        //            name:'test5',
        //            type:'line',
        //            yAxisIndex: 1,
        //            lineStyle: {
        //                normal: {
        //                    color:'#c39705'
        //                }
        //            },
        //            data:[30, 10, 90,75]
        //        }
        //    ]
        //};
        //edubalance.setOption(option);

        /* 地图 需要哪个省市的地图直接引用js 将下面的name以及mapType修改为对应省市名称*/
        //var maps = echarts.init(document.getElementById('mapadd'));
        //option = {
        //    tooltip: {
        //        trigger: 'item',
        //        formatter: '{b}'
        //    },
        //    series: [{
        //        name: '山东',
        //        type: 'map',
        //        mapType: '山东',
        //        roam: false,
        //        top: '8%',
        //        left: '10%',
        //        zoom: 1.2,
        //        x: '22%',
        //        selectedMode: 'single',//multiple多选
        //        itemStyle: {
        //            normal: {
        //                label: {
        //                    show: true,
        //                    textStyle: {
        //                        color: "#231816"
        //                    }
        //                },
        //                areaStyle: { color: '#B1D0EC' },
        //                color: '#B1D0EC',
        //                borderColor: '#dadfde'//区块的边框颜色
        //            },
        //            emphasis: {//鼠标hover样式
        //                label: {
        //                    show: true,
        //                    textStyle: {
        //                        color: '#fa4f04'
        //                    }
        //                },
        //                areaStyle: { color: 'red' }
        //            }
        //        },
        //        data: [
        //            {
        //                name: '济南市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: '#13d5ff',
        //                        borderColor: '#edce00'
        //                    }
        //                }
        //            },
        //            {
        //                name: '德州市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: '#13d5ff',
        //                        borderColor: '#0abcee'
        //                    }
        //                }
        //            },
        //            {
        //                name: '聊城市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: '#92d050',
        //                        borderColor: '#1ca9f2'
        //                    }
        //                }
        //            },
        //            {
        //                name: '泰安市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: '#88ddf5',
        //                        borderColor: '#88ddf5',
        //                    }
        //                }
        //            },
        //            {
        //                name: '莱芜市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: '#13d5ff',
        //                        borderColor: '#45b5ef',
        //                    }
        //                }
        //            },
        //            {
        //                name: '菏泽市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: '#13d5ff',
        //                        borderColor: '#45b5ef'
        //                    }
        //                }
        //            },
        //            {
        //                name: '枣庄市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: '#45b5ef',
        //                        borderColor: '#45b5ef',
        //                    }
        //                }
        //            },
        //            {
        //                name: '济宁市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: '#ffd811',
        //                        borderColor: '#45b5ef',
        //                    }
        //                }
        //            },
        //            {
        //                name: '临沂市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: '#ffa312',
        //                        borderColor: '#45b5ef',
        //                    }
        //                }
        //            },
        //            {
        //                name: '青岛市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: '#92d050',
        //                        borderColor: '#1ca9f2'
        //                    }
        //                }
        //            },
        //            {
        //                name: '烟台市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: '#88ddf5',
        //                        borderColor: '#88ddf5',
        //                    }
        //                }
        //            },
        //            {
        //                name: '威海市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: '#13d5ff',
        //                        borderColor: '#45b5ef',
        //                    }
        //                }
        //            },
        //            {
        //                name: '潍坊市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: '#13d5ff',
        //                        borderColor: '#45b5ef'
        //                    }
        //                }
        //            },
        //            {
        //                name: '淄博市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: '#45b5ef',
        //                        borderColor: '#45b5ef',
        //                    }
        //                }
        //            },
        //            {
        //                name: '滨州市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: '#13d5ff',
        //                        borderColor: '#45b5ef',
        //                    }
        //                }
        //            },
        //            {
        //                name: '东营市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: 'red',
        //                        borderColor: '#45b5ef',
        //                    }
        //                }
        //            },
        //            {
        //                name: '日照市',
        //                itemStyle: {
        //                    normal: {
        //                        areaColor: 'red',
        //                        borderColor: '#45b5ef',
        //                    }
        //                }
        //            },
        //        ]
        //    }]
        //};
        //maps.setOption(option);





        //function requestProductData() {
        //    $.getJSON('../Handler/ProcessHandler.ashx?action=l02mf', function (data) {
        //        debugger;
        //        if (data.hasError == true) {
        //            alert(data.error);
        //            return;
        //        }
        //        else {
        //            initechart();
        //            //var graduateyear = graduateyear_option.getOption();
        //            //graduateyear.setOption(graduateyear_option);
        //            d = JSON.parse(data.data);
        //            //var names = [];
        //            //var series = [{ name: '订单计划量', data: [] }, { name: '订单完成量', data: [] }];

        //            //series[0].data.push([Date.UTC(2010, 0, 1), 100]);
        //            //series[1].data.push([Date.UTC(2010, 0, 1), 80]);


        //            //series[0].data.push([Date.UTC(2010, 0, 2), 100]);
        //            //series[1].data.push([Date.UTC(2010, 0, 2), 100]);

        //            for (m in d) {
        //                //var date = new Date(d[m].WorkDate);
        //                //var dd = Date.UTC(date.getFullYear(), date.getMonth(), date.getDate());
        //                graduateyear_option.series[0].data.push(d[m].value);
        //                graduateyear_option.xAxis[0].data.push(new Date(d[m].time).Format("HH:mm:ss"));
        //                //series[1].data.push([d[m].TSNumber, d[m].FinishQty]);

        //            }

        //            //graduateyear_option.series[0].data = d.value;
        //            ////graduateyear_option.series[1].data = d.FinishQty;
        //            //graduateyear_option.yAxis[0].data = d.time;

        //            graduateyear.setOption(graduateyear_option)
        //            //if (graduateyear_option.series.length == 0) {
        //            //    for (i in series) {
        //            //        graduateyear_option.addSeries({
        //            //            name: series[i].name,
        //            //            data: series[i].data
        //            //        });
        //            //    }
        //            //} else {
        //            //    graduateyear_option.update({
        //            //        series: series
        //            //    });
        //            //}


        //        }
        //    });
        //}




        //Date.prototype.Format = function (fmt) {
        //    var o = {
        //        "M+": this.getMonth() + 1, //月份 
        //        "d+": this.getDate(), //日 
        //        "H+": this.getHours(), //小时 
        //        "m+": this.getMinutes(), //分 
        //        "s+": this.getSeconds(), //秒 
        //        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        //        "S": this.getMilliseconds() //毫秒 
        //    };
        //    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        //    for (var k in o)
        //        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        //    return fmt;

        //}


        //requestProductData()
        //setInterval(requestProductData, 10000)








//    })
