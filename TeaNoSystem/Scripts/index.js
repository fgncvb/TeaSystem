/**
 * Created by sp450 on 2017/12/21.
 */
//JavaScript代码区域
layui.use('element', function(){
    var element = layui.element;
});
// 日期
layui.use('laydate', function(){
    var laydate = layui.laydate;

    //执行一个laydate实例
    laydate.render({
        elem: '#startTime' //指定元素
    });
    laydate.render({
        elem: '#endTime' //指定元素
    });
    laydate.render({
        elem: '#timeFrame'
        ,range: true
    });
    laydate.render({
        elem: '#publishingTime' //出版日期
    });
    laydate.render({
        elem: '#creationDate' //创建日期
    });
    laydate.render({
        elem: '#pressDate' //出版社时间
    });
});
// 分页
layui.use('laypage', function(){
    var laypage = layui.laypage;

    //执行一个laypage实例
    laypage.render({
        elem: 'tableTotalPaging' //注意，这里的 test1 是 ID，不用加 # 号
        ,count: 80 //数据总数，从服务端得到
    });
});
// 弹出
// 删除
    $('.deleteList').on('click', function(){
        layer.confirm('<span style="font-size: 16px">确定删除吗？</span>', { icon: 3,title:'<span style="display: inline-block;">删&nbsp;除</span>'}, function(index){
            //do something
            layer.close(index);
        });
    });
$('.NavShrinkIcon').click(function () {
    console.log( $('.leftList').css('width'))
    if(    $('.leftList').css('width')=='200px'){
        $('.layui-layout-admin .layui-side').css('width','45px');
        $('.NavShrinkIcon').css('margin-right','170px');
        $('.ShrinklistIcon').css('margin-right','10px');
        $('.layui-body').css('left','50px');
        $('.leftListTitle').css('display','none');
        $('.leftListTitleItem').addClass('width45')
        $('.leftList').addClass('width45')
        $('.leftList').removeClass('layui-nav-tree')
        $('.layui-nav-more').css('display','none')
        $('.leftListTitleDl ').css({'left':'45px','top':'10px'})
        $('.leftListTitleItem').removeClass('layui-nav-itemed')
    }else {
        $('.layui-layout-admin .layui-side').css('width','200px');
        $('.NavShrinkIcon').css('margin-right','10px');
        $('.ShrinklistIcon').css('margin-right','0px');
        $('.layui-body').css('left','200px');
        $('.leftListTitle').css('display','inline-block');
        $('.leftListTitleItem').css('width','200px')
        $('.leftList').removeClass('width45');
        $('.leftList').addClass('layui-nav-tree')
        $('.layui-nav-more').css('display','block')
        $('.leftListTitleDl ').css({'left':'0','top':'0px'})
    }

});
// 编辑
$('.editList').on('click', function(){
    window.open("editData.html")
});
// 新增
$('.tableAddButton').on('click', function(){
    window.open("addNewData.html")
});
// 系统角色管理
$('.systemRoleEditList').on('click', function(){
    window.open("systemRoleEditData.html")
});
$('.systemAddButton').on('click', function(){
    window.open("systemRoleAddNewData.html")
});
// 角色管理
$('.roleEditList').on('click', function(){
    window.open("roleEditData.html")
});
$('.roleTableAddButton').on('click', function(){
    window.open("roleAddNewData.html")
});
// 权限管理
$('.jurisdictionEditList').on('click', function(){
    window.open("jurisdictionpathEditData.html")
});
$('.jurisdictionTableAddButton').on('click', function(){
    window.open("jurisdictionAddNewData.html")
});
// 路径
$('.pathEditList').on('click', function(){
    window.open("pathEditData.html")
});
$('.pathAddButton').on('click', function(){
    window.open("pathAddNewData.html")
});
// 图书
$('.bookEditList').on('click', function(){
    window.open("bookEditData.html")
});
$('.bookAddButton').on('click', function(){
    console.log('sss')
    window.open("bookAddNewData.html")
});
// 图书类型
$('.typeBookEditList').on('click', function(){
    window.open("typebookEditData.html")
});
$('.typeBookAddButton').on('click', function(){
    window.open("typebookAddNewData.html")
});
// 详情
$('.bookEditdetails').on('click',function () {
    window.open("bookEditdetails.html")
})
//出版社
$('.pressEditList').on('click', function(){
    window.open("pressEditData.html")
});
$('.pressAddButton').on('click', function(){
    window.open("pressAddNewData.html")
});

// 上传
layui.use('upload', function() {
    var $ = layui.jquery
        , upload = layui.upload;
    //普通图片上传
    var uploadInst = upload.render({
        elem: '#test1'
        ,url: '/upload/'
        ,before: function(obj){
            //预读本地文件示例，不支持ie8
            obj.preview(function(index, file, result){
                $('#demo1').attr('src', result); //图片链接（base64）
            });
        }
        ,done: function(res){
            //如果上传失败
            if(res.code > 0){
                return layer.msg('上传失败');
            }
            //上传成功
        }
        ,error: function(){
            //演示失败状态，并实现重传
            var demoText = $('#demoText');
            demoText.html('<span style="color: #FF5722;">上传失败</span> <a class="layui-btn layui-btn-mini demo-reload">重试</a>');
            demoText.find('.demo-reload').on('click', function(){
                uploadInst.upload();
            });
        }
    });

})
// 提交
$('.listButton').on('click',function () {
    layer.confirm('<span style="font-size: 16px">确认提交吗？</span>', {icon: 3,
        btn: ['提交','取消'] //按钮
    },function(){
        layer.msg('提交成功', {
            time: 2000, //20s后自动关闭
        });
    });
})
// 退出
$('.Closelogon').on('click',function () {
    layer.confirm('<span style="font-size: 16px">确认退出吗？</span>', {icon: 3,
        btn: ['退出','取消'] //按钮
    },function(index){
        layer.close(index);
    });
})
// 全选
$('#checkAllInput').click(function () {
    if($('input[name="checkAllInput"]').prop("checked")){
        $(".checkBoxa").each(function(){
            $(this).prop("checked",true);
        })
    }else {
        console.log('未选中')
        $(".checkBoxa").each(function(){
            $(this).prop("checked",false);
        })

    }
})

// 焦点显示
$('.InputscreenInput').focus(function () {
    $('.Inputscreen').show();
})
$('.InputscreenInput').blur(function () {
    $('.Inputscreen').hide();
})
// 左侧导航

// 双击变成文本框
function ShowElement(element) {
    var oldhtml = element.innerHTML;
    var newobj = document.createElement('input');
    newobj.setAttribute("className", "inpuClass");
    newobj.type = 'text';
    newobj.value = oldhtml;
    newobj.onblur = function() {
        element.innerHTML = this.value == oldhtml ? oldhtml : this.value;
        element.setAttribute("ondblclick", "ShowElement(this);");
    };
    element.innerHTML = '';
    element.appendChild(newobj);
    newobj.setSelectionRange(0, oldhtml.length);
    newobj.focus();
    newobj.parentNode.setAttribute("ondblclick", "");
    var testLength = newobj.value.length;
    newobj.style.width=testLength*16+'px';
}
// 判断ifrom页面状态
$('.listAddCatalog').click(function () {

});
// 显示隐藏 添加子节点;
$('.listAddCatalog').on("mouseover",'.addlevel',function () {
    $(this).find('.addChapter').show();
});
$('.listAddCatalog').on("mouseout",'.addlevel',function () {
    $(this).find('.addChapter').hide()
})
// 点击子节点
$('.addChapter').click(function (e) {
    if ( e && e.stopPropagation )
        e.stopPropagation();
    else{
        window.event.cancelBubble = true;
    return false;
    }
});

//联级查询
layui.use(['form', 'layedit'], function () {
    var form = layui.form,
        layedit = layui.layedit,
        $ = layui.jquery,
        layer = layui.layer;


    form.on('select(tid)', function (data) {

        var data = { level: 2, parentid: data.value };

        $.ajax({
            url: '/Base/GetAreaList',
            data: data,
            type: 'post',
            success: function (datas) {
                $("select[name=cbcity]").html('<option value="">请选择</option>'); //清空
                $("select[name=cbcounty]").html('<option value="">请选择</option>'); //清空
                $("select[name=cbschool]").html('<option value="">请选择</option>'); //清空
                $("select[name=cbgrade]").html('<option value="">请选择</option>'); //清空
                $("select[name=cbclass]").html('<option value="">请选择</option>'); //清空

                $("select[name=cbcity]").append(datas);
                form.render('select');
            }

        });

    });


    form.on('select(cbcity)', function (data) {
        var data = { level: 3, parentid: data.value };

        $.ajax({
            url: '/Base/GetAreaList',
            data: data,
            type: 'post',
            success: function (datas) {
                $("select[name=cbcounty]").html('<option value="">请选择</option>'); //清空
                $("select[name=cbschool]").html('<option value="">请选择</option>'); //清空
                $("select[name=cbgrade]").html('<option value="">请选择</option>'); //清空
                $("select[name=cbclass]").html('<option value="">请选择</option>'); //清空
                $("select[name=cbcounty]").append(datas);
                form.render('select');
            }

        });
    });
    form.on('select(cbcounty)', function (data) {

        var school_data = {
            provinceid: $("select[name=tid]").val(),
            cityid: $("select[name=cbcity]").val(),
            countyid: $("select[name=cbcounty]").val()
        };

        $.ajax({
            url: "/Base/GetSchoolList",
            data: school_data,
            type: 'post',
            success: function (result) {
                $("select[name=cbschool]").html('<option value="">请选择</option>'); //清空
                $("select[name=cbgrade]").html('<option value="">请选择</option>'); //清空
                $("select[name=cbclass]").html('<option value="">请选择</option>'); //清空
                $("select[name=cbschool]").append(result);
                form.render('select');
            }
        });
    });

    form.on('select(cbschool)', function (data) {

        var grade_data = {
            schoolid: $("select[name=cbschool]").val()
        };

        $.ajax({
            type: "post",
            url: "/Base/GetGradeList",
            data: grade_data,
            success: function (result) {
                if (result != "") {
                    $("select[name=cbgrade]").html('<option value="">请选择</option>'); //清空
                    $("select[name=cbclass]").html('<option value="">请选择</option>'); //清空
                    $("select[name=cbgrade]").append(result);
                    form.render('select');
                }
            }
        });
    });


    form.on('select(cbgrade)', function (data) {

        var grade_data = {
            schoolid: $("select[name=cbschool]").val(),
            gradeid: $("select[name=cbgrade]").val()

        };

        $.ajax({
            type: "post",
            url: "/Base/GetClassList",
            data: grade_data,
            success: function (result) {
                if (result != "") {
                    $("select[name=cbclass]").html('<option value="">请选择</option>'); //清空
                    $("select[name=cbclass]").append(result);
                    form.render('select');
                }
            }
        });
    });

});
