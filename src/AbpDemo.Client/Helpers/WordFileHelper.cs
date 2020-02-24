using System;
using System.Collections.Generic;
using System.Text;
using Aspose.Words;
using Aspose.Words.Tables;
using AbpDemo.Business;
using System.Drawing;

namespace AbpDemo.Client
{
    public class WordFileHelper
    {
        public static void BuildFile(string savePath,string templatePath= "C:\\Users\\Administrator\\Desktop\\Template.docx")
        {
            Document doc = new Document(templatePath);//文档示例
            DocumentBuilder builder = new DocumentBuilder(doc);//文档构造器

            Bookmark bookmark1 = doc.Range.Bookmarks["CustomTable"];
            if (bookmark1 != null)
            {
                builder.MoveToBookmark("CustomTable");//移动至书签
                DbTable custom = MakeCustomData();//生成测试数据
                CreateCustomTable(doc, builder, custom);//创建表格
                bookmark1.Remove();//移除书签
            }

            Bookmark bookmark2 = doc.Range.Bookmarks["TreeTable"];
            if (bookmark2 != null)
            {
                builder.MoveToBookmark("TreeTable");//移动至书签
                List<Classify> tree = MakeTreeData();//生成测试数据
                CreateTreeTable(doc, builder, tree);//创建表格
                bookmark2.Remove();//移除书签
            }

            Bookmark bookmark3 = doc.Range.Bookmarks["ListTable"];
            if (bookmark3 != null)
            {
                builder.MoveToBookmark("ListTable");//移动至书签
                List<GoodsModel> list = MakeListData();//生成测试数据
                CreateSimpleTable(doc, builder, list);//创建表格
                bookmark3.Remove();//移除书签
            }


            doc.Save(savePath);//保存文档
        }

        private static void CreateSimpleTable(Document doc,DocumentBuilder builder,List<GoodsModel> list)
        {
            Table table = builder.StartTable();

            #region 表头
            string[] titles = new string[] { "序号", "货品名称", "货品类型", "存放位置", "数量" };
            int[] lens = new int[] { 10, 25, 25, 25, 15 };
            for (int i=0;i<5;i++)
            {
                builder.InsertCell();//插入单元格
                builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[i]);//列宽-百分比
                builder.CellFormat.Shading.BackgroundPatternColor = Color.LightGray;//背景色-灰色
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐-居中
                builder.Write(titles[i]);//写入内容

            }
            builder.EndRow();//结束行
            #endregion

            #region 内容
            for (int i=0;i<list.Count;i++)
            {
                GoodsModel model = list[i];

                builder.InsertCell();//插入单元格
                builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[0]);//列宽
                builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色-白色
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐-居中
                builder.Write((i + 1).ToString());//写入内容

                builder.InsertCell();//插入单元格
                builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[1]);//列宽
                builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色-白色
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;//对齐-靠左
                builder.Write(model.GoodsName);//写入内容

                builder.InsertCell();//插入单元格
                builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[2]);//列宽
                builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色-白色
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐-居中
                builder.Write(model.GoodsType);//写入内容

                builder.InsertCell();//插入单元格
                builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[3]);//列宽
                builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色-白色
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Right;//对齐-靠右
                builder.Write(model.Location);//写入内容

                builder.InsertCell();//插入单元格
                builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[4]);//列宽
                builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色-白色
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐-居中
                builder.Write((model.GoodsNum).ToString());//写入内容

                builder.EndRow();//结束行
            }
            #endregion

            builder.EndTable();//结束表格
        }

        private static void CreateTreeTable(Document doc,DocumentBuilder builder,List<Classify> tree)
        {
            Table table = builder.StartTable();

            #region 表头
            string[] titles = new string[] { "序号", "大类名称", "大类编码", "小类名称", "小类编码", "细类名称", "细类编码" };
            int[] lens = new int[] { 4, 16, 16, 16, 16, 16, 16 };
            for (int i = 0; i < 7; i++)
            {
                builder.InsertCell();//插入单元格
                builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[i]);//列宽-百分比
                builder.CellFormat.Shading.BackgroundPatternColor = Color.LightGray;//背景色-灰色
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐-居中
                builder.Write(titles[i]);//写入内容

            }
            builder.EndRow();//结束行
            #endregion
            int index = 1;//序号

            for (int i=0;i<tree.Count; i++)
            {
                Classify node1 = tree[i];
                int num1 = 0;

                for (int j=0;j<node1.Children.Count;j++)
                {
                    Classify node2 = node1.Children[j];
                    int num2 = 0;

                    for (int k=0;k<node2.Children.Count;k++)
                    {
                        Classify node3 = node2.Children[k];

                        builder.InsertCell();
                        builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[0]);//列宽
                        builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐
                        builder.Write(index.ToString());//写入内容

                        builder.InsertCell();
                        builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[1]);//列宽
                        builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐
                        if (num1 == 0)
                        {
                            builder.Write(node1.ClassifyName);//写入内容
                            builder.CellFormat.VerticalMerge = CellMerge.First;
                        }
                        else
                        {
                            builder.CellFormat.VerticalMerge = CellMerge.Previous;
                        }

                        builder.InsertCell();
                        builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[2]);//列宽
                        builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐
                        if (num1 == 0)
                        {
                            builder.Write(node1.ClassifyCode);//写入内容
                            builder.CellFormat.VerticalMerge = CellMerge.First;
                        }
                        else
                        {
                            builder.CellFormat.VerticalMerge = CellMerge.Previous;
                        }


                        builder.InsertCell();
                        builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[3]);//列宽
                        builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐
                        if (num2 == 0)
                        {
                            builder.Write(node2.ClassifyName);//写入内容
                            builder.CellFormat.VerticalMerge = CellMerge.First;
                        }
                        else
                        {
                            builder.CellFormat.VerticalMerge = CellMerge.Previous;
                        }

                        builder.InsertCell();
                        builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[4]);//列宽
                        builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐
                        if (num2 == 0)
                        {
                            builder.Write(node2.ClassifyCode);//写入内容
                            builder.CellFormat.VerticalMerge = CellMerge.First;
                        }
                        else
                        {
                            builder.CellFormat.VerticalMerge = CellMerge.Previous;
                        }

                        builder.InsertCell();
                        builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[5]);//列宽
                        builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐
                        builder.CellFormat.VerticalMerge = CellMerge.None;
                        builder.CellFormat.HorizontalMerge = CellMerge.None;
                        builder.Write(node3.ClassifyName);//写入内容

                        builder.InsertCell();
                        builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[6]);//列宽
                        builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色
                        builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐
                        builder.CellFormat.VerticalMerge = CellMerge.None;
                        builder.CellFormat.HorizontalMerge = CellMerge.None;
                        builder.Write(node3.ClassifyCode);//写入内容

                        builder.EndRow();

                        index++;
                        num1++;
                        num2++;
                    }
                }
            }

            #region 内容

            #endregion

            builder.EndTable();
        }


        private static void CreateCustomTable(Document doc,DocumentBuilder builder,DbTable custom)
        {
            Table table = builder.StartTable();

            #region 数据表信息
            string[] titles = new string[] { "表名称", "英文名称", "描述" };
            string[] values = new string[] { custom.TableName, custom.EnglishName, custom.TableDescribe };
            int[] lens = new int[] { 6, 25, 25, 12, 14, 18 };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    builder.InsertCell();
                    builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[j]);
                    builder.CellFormat.Shading.BackgroundPatternColor = Color.LightGray;
                    builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                    if (j == 0)
                    {
                        builder.Write(titles[i]);
                        builder.CellFormat.HorizontalMerge = CellMerge.First;
                    }
                    else if (j >0&&j<3)
                    {
                        builder.CellFormat.HorizontalMerge = CellMerge.Previous;
                    }
                    else if (j == 3)
                    {
                        builder.Write(values[i]);
                        builder.CellFormat.HorizontalMerge = CellMerge.First;
                    }
                    else
                    {
                        builder.CellFormat.HorizontalMerge = CellMerge.Previous;
                    }
                }
                builder.EndRow();
            }
            #endregion

            #region 数据列表头
            string[] colTiltes = new string[] { "序号", "列名", "英文名", "数据类型", "长度", "描述" };
            for (int i = 0; i < 6; i++)
            {
                builder.InsertCell();//插入单元格
                builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[i]);//列宽-百分比
                builder.CellFormat.Shading.BackgroundPatternColor = Color.Gray;//背景色-灰色
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐-居中
                builder.Write(colTiltes[i]);//写入内容

            }
            builder.EndRow();//结束行
            #endregion

            #region 数据列内容
            for (int i = 0; i < custom.Columns.Count; i++)
            {
                DbColumn model = custom.Columns[i];

                builder.InsertCell();//插入单元格
                builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[0]);//列宽
                builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色-白色
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐-居中
                builder.Write((i + 1).ToString());//写入内容

                builder.InsertCell();//插入单元格
                builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[1]);//列宽
                builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色-白色
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;//对齐-靠左
                builder.Write(model.ColumnName);//写入内容

                builder.InsertCell();//插入单元格
                builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[2]);//列宽
                builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色-白色
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐-居中
                builder.Write(model.EnglishName);//写入内容

                builder.InsertCell();//插入单元格
                builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[3]);//列宽
                builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色-白色
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Right;//对齐-靠右
                builder.Write(model.ColumnType);//写入内容

                builder.InsertCell();//插入单元格
                builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[4]);//列宽
                builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色-白色
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐-居中
                builder.Write((model.ColumnLength).ToString());//写入内容

                builder.InsertCell();//插入单元格
                builder.CellFormat.PreferredWidth = PreferredWidth.FromPercent(lens[4]);//列宽
                builder.CellFormat.Shading.BackgroundPatternColor = Color.White;//背景色-白色
                builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;//对齐-居中
                builder.Write(model.ColumnDescribe);//写入内容

                builder.EndRow();//结束行
            }

            #endregion

            builder.EndTable();
        }

        private static List<GoodsModel> MakeListData()
        {
            List<GoodsModel> list = new List<GoodsModel>();

            GoodsModel model1 = new GoodsModel { GoodsName = "矿泉水", GoodsType = "酒水饮料", Location = "1号库", GoodsNum = 100 };
            GoodsModel model2 = new GoodsModel { GoodsName = "纯净水", GoodsType = "酒水饮料", Location = "1号库", GoodsNum = 400 };
            GoodsModel model3 = new GoodsModel { GoodsName = "苏打水", GoodsType = "酒水饮料", Location = "1号库", GoodsNum = 200 };
            GoodsModel model4 = new GoodsModel { GoodsName = "啤酒", GoodsType = "酒水饮料", Location = "1号库", GoodsNum = 100 };
            list.Add(model1);
            list.Add(model2);
            list.Add(model3);
            list.Add(model4);
            return list;
        }
        

        private static List<Classify> MakeTreeData()
        {
            List<Classify> tree = new List<Classify>();
            Classify node_1 = new Classify { ClassifyName = "酒水饮料", ClassifyCode = "01", ClassfyLevel = 1 };
            Classify node_1_1 = new Classify { ClassifyName = "白酒", ClassifyCode = "0101", ClassfyLevel = 2 };
            Classify node_1_2 = new Classify { ClassifyName = "红酒", ClassifyCode = "0102", ClassfyLevel = 2 };
            Classify node_1_3 = new Classify { ClassifyName = "啤酒", ClassifyCode = "0103", ClassfyLevel = 2 };
            Classify node_1_1_1 = new Classify { ClassifyName = "茅台", ClassifyCode = "010101", ClassfyLevel = 3 };
            Classify node_1_1_2 = new Classify { ClassifyName = "五粮液", ClassifyCode = "010102", ClassfyLevel = 3 };
            Classify node_1_2_1 = new Classify { ClassifyName = "法国", ClassifyCode = "010201", ClassfyLevel = 3 };
            Classify node_1_2_2 = new Classify { ClassifyName = "澳大利亚", ClassifyCode = "010202", ClassfyLevel = 3 };
            Classify node_1_3_1 = new Classify { ClassifyName = "黑啤", ClassifyCode = "010301", ClassfyLevel = 3 };
            Classify node_1_3_2 = new Classify { ClassifyName = "黄啤", ClassifyCode = "010302", ClassfyLevel = 3 };
            node_1_1.Children = new List<Classify> { node_1_1_1, node_1_1_2 };
            node_1_2.Children = new List<Classify> { node_1_2_1, node_1_2_2 };
            node_1_3.Children = new List<Classify> { node_1_3_1, node_1_3_2 };
            node_1.Children = new List<Classify> { node_1_1, node_1_2, node_1_3 };
            tree.Add(node_1);

            Classify node_2 = new Classify { ClassifyName = "食品生鲜", ClassifyCode = "02", ClassfyLevel = 1 };
            Classify node_2_1 = new Classify { ClassifyName = "新鲜水果", ClassifyCode = "0201", ClassfyLevel = 2 };
            Classify node_2_2 = new Classify { ClassifyName = "蔬菜蛋品", ClassifyCode = "0202", ClassfyLevel = 2 };
            Classify node_2_3 = new Classify { ClassifyName = "海鲜水产", ClassifyCode = "0203", ClassfyLevel = 2 };
            Classify node_2_1_1 = new Classify { ClassifyName = "苹果", ClassifyCode = "020101", ClassfyLevel = 3 };
            Classify node_2_1_2 = new Classify { ClassifyName = "菠萝", ClassifyCode = "020102", ClassfyLevel = 3 };
            Classify node_2_2_1 = new Classify { ClassifyName = "西红柿", ClassifyCode = "020201", ClassfyLevel = 3 };
            Classify node_2_2_2 = new Classify { ClassifyName = "包菜", ClassifyCode = "020202", ClassfyLevel = 3 };
            Classify node_2_3_1 = new Classify { ClassifyName = "虾", ClassifyCode = "020301", ClassfyLevel = 3 };
            Classify node_2_3_2 = new Classify { ClassifyName = "蟹", ClassifyCode = "020302", ClassfyLevel = 3 };
            node_2_1.Children = new List<Classify> { node_2_1_1, node_2_1_2 };
            node_2_2.Children = new List<Classify> { node_2_2_1, node_2_2_2 };
            node_2_3.Children = new List<Classify> { node_2_3_1, node_2_3_2 };
            node_2.Children = new List<Classify> { node_2_1, node_2_2, node_2_3 };
            tree.Add(node_2);
            return tree;
        }

        private static DbTable MakeCustomData()
        {
            DbTable table = new DbTable { TableName = "货品信息表", EnglishName = "GoodsInfo", TableDescribe = "描述货品基本信息" };
            DbColumn column1 = new DbColumn { ColumnName = "货品名称", EnglishName = "GoodsName", ColumnType = "文本", ColumnLength = 50, ColumnDescribe = "" };
            DbColumn column2 = new DbColumn { ColumnName = "货品类型", EnglishName = "GoodsType", ColumnType = "文本", ColumnLength = 50, ColumnDescribe = "货品分类名称" };
            DbColumn column3 = new DbColumn { ColumnName = "存放位置", EnglishName = "Location", ColumnType = "文本", ColumnLength = 50, ColumnDescribe = "" };
            DbColumn column4 = new DbColumn { ColumnName = "货品数量", EnglishName = "GoodsNum", ColumnType = "整型", ColumnLength = 10, ColumnDescribe = "" };
            table.Columns = new List<DbColumn> { column1, column2, column3, column4 };
            return table;
        }
    }
}
