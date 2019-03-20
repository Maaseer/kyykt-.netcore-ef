Vue.component('item_通知',{
	data:function(){
		return{
			items:[],
			url:'api/notice/',
			classId:window.localStorage.getItem("ClassId"),
			newItemHead:'',
		}
	},
	methods:{
		getData(){
			var vm = this;
			axios.get(vm.url+vm.classId)
			.then(function(response){

				vm.items = response.data;

			})
		},
		deleteitem(e){
			var vm = this;
			var json={
				data:{
					ClassId:e.classId,
					noticeId:e.noticeId
				}
			}
			axios.delete(vm.url,json).then(function(response){
				console.log("删除成功");
				vm.getData();
			});
		},
		newItem(){
			var vm = this;
			console.log("111");
			if(vm.newItemHead!=''){
				axios.put(vm.url,{
					classId:vm.classId,
					head:vm.newItemHead,
					
				}).then(function(response){
					vm.newItemHead='';
					vm.getData();
				})
			}
		}
	},
	created(){
		this.getData();
	},
	watch: {
		classId:function(){
			this.getData();
		}
	},
	template:'\
	<div class="row" id="notice-list" >\
		<notice-item v-for="item in items" v-bind:key="item.noticeId" v-bind:item="item" v-on:deleteitem="deleteitem"></notice-item>\
		<div class="col-md-5">\
			<div class="input-group">\
				<input name="input1-group2" class="form-control" id="input-group-2" type="text" placeholder="通知标题" v-model="newItemHead" @blur="newItem">\
				<span class="input-group-btn">\
					<button class="btn btn-primary" type="button" ><i class="fa fa-search"></i> 创建</button>\
				</span>\
			</div>\
		</div>\
	</div>\
	',				
});
Vue.component('notice-item',{
	props:['item'],
	  template:'\
	  	<div class="col-md-5">\
			<div class="card">\
				<div class="card-header bg-light" v-bind:style="card_head">\
					<span v-on:click="setHead">{{item.head}}</span>\
					<div class="card-actions">\
						<a class="btn" v-on:click="setTextArea">\
							<i class="fa fa-pencil-alt"></i>\
						</a>\
						<a class="btn" v-on:click="$emit(\'deleteitem\',item)">\
							<i class="fa fa-cog"></i>\
						</a>\
					</div>\
				</div>\
				<div class="input-group" v-bind:style="input">\
					<input name="input1-group2" class="form-control" id="input-group-2" type="text" placeholder="通知标题" v-model="item.head" @blur="setHead">\
					<span class="input-group-btn">\
						<button class="btn btn-primary" type="button" ><i class="fa fa-search"></i> 修改</button>\
					</span>\
				</div>\
				<div class="card-body" v-bind:style="card_body">\
					{{item.content}}</br></br></br><small>发布时间:{{item.time}}</small>\
				</div>\
				<textarea v-model="item.content" class="form-control" v-bind:style="TextArea" @blur="setTextArea"></textarea>\
			</div>\
		</div>\
	  ',
	 data:function(){
		 return{
			 TextArea:{
				 display:'none'
			 },
			 card_body:{
				 display:''
			 },
			 card_head:{
				 display:''
			 },
			 input:{
				 display:'none'
			 },
			 url:'api/notice/',
		 }
	 } ,
	 methods:{
			setTextArea(){
				if(this.TextArea.display=='none')
				{
					this.TextArea.display='';
					this.card_body.display = 'none';
				}
				else{
					this.TextArea.display='none';
					this.card_body.display = '';
					this.submitText();
				}
			},
			submitText(){
				var vm = this;
				var data = vm._props.item;
				axios.post(vm.url,{
					classId:data.classId,
					noticeId:data.noticeId,
					content:data.content,
					head:data.head,
					time:data.time
				}).then(function(response){
					alert("已自动保存");

				})
			},				
			setHead(){
				if(this.input.display=='none')
				{
					this.input.display='';
					this.card_head.display = 'none';
				}
				else{
					this.input.display='none';
					this.card_head.display = '';
					this.submitText();
				}
			},
	  }

  });
Vue.component('item_签到',{
    data:function(){
        return{
            items:[],
            url : 'api/SignIn/',
            TeacherId:window.localStorage.getItem("teacherId"),  
        }
    },

    methods: {
		//停止签到
	  stopSignIn: function(e){
		var vm = this;
		alert("停止签到成功！");
		axios.get('/api/QrCode',{
			params:{
				classId:e.classId
			}
		}).then(function(response){
			axios.delete('/api/QrCode/' + response.data.qrCodeStr)
			.then(function(response){
				vm.getData();
			})
		})
		},
		//开始签到
		begainSignIn:function(e){
			var vm = this;
			if (confirm("确定开始本课程点名？")) {
				axios.put('/api/QrCode',{
					classId:e.classId
				}
				).then(function(response){
					
					window.localStorage.setItem("SignInClassId",e.classId);
					window.localStorage.setItem("SignInClassTimes",e.signInTimes);
					window.setTimeout(vm.getData,1000);
					window.open('testvue.html');
					
				})
			}  
			else {  
				alert("点名已取消");  
			} 
		},
		//组件加载时获得数据
		getData(){
			var vm = this;
			var url = vm.url + vm.TeacherId;
			axios.get(url)
			.then(function(response){
				response.data.forEach(element => {
					element.classTime='';
					element.classTimes.forEach(element1 => {
						element.classTime = element.classTime + element1.place + '|';
					});
					element.classTime=element.classTime.substring(0,element.classTime.length-1)
				});
				vm.items = response.data;
		})	
		},
		goon(e){
			window.localStorage.setItem("SignInClassId",e.classId);
			window.localStorage.setItem("SignInClassTimes",e.signInTimes);
			window.open('testvue.html');
		}
	},
	created(){
		this.getData();
	}

    
    ,
    template:'\
    <div class="row">\
        <div class="col-md-12">\
            <div class="card">\
                    <div class="card-header bg-light">教师课表</div>\
                    <div class="card-body">\
                        <div class="table-responsive">\
                            <table class="table table-hover">\
                                <thead>\
                                    <tr>\
                                        <td>课程号</td>\
                                        <td>课程名</td>\
                                        <td>签到次数</td>\
                                        <td>上课地点</td>\
                                        <td>签到状态</td>\
                                        <td></td>\
                                    </tr>\
                                </thead>\
                                <tbody>\
                                    <tr is="class-item" v-for="item in items" v-bind:key="item.classId" v-bind:item="item" v-on:stop="stopSignIn" v-on:begain="begainSignIn" v-on:goon="goon">\
                                    </tr>\
                                </tbody>\
                            </table>\
                        </div>\
                    </div>\
                </div>\
            </div> \
    </div>\
    ',
});
Vue.component('item_首页',{
    data:function(){
        var that = this;
        that.t1=window.setInterval(refreshCount, 1000);
        function refreshCount() {
          that.date = new Date();
        }
        return{
            message:window.localStorage.getItem("name"),
            date:new Date(),
            t1:null
        }
    },
    template:'<div class="jumbotron">\
	<h1>欢迎您，{{message}}老师</h1> \
	<p>现在是，{{date}}</p> \
  </div>\
',	
});
Vue.component('class-item', {
	template: '\
		<tr v-bind:class="{NoSignIn:!item.isInSign}" v-bind:id="item.classId">\
			<td>{{item.classId}}</td>\
			<td class="text-nowrap">{{item.className}}</td>\
			<td>{{item.signInTimes}}</td>\
			<td>\
				\
				<!--<ul>\
					<li v-for="i in item.classTimes">{{i.place}}</li>\
				</ul>-->\
				{{item.classTime}}\
			</td>\
			<td>{{item.isInSign?\'结束签到\':\'正在签到\'}}</td>\
			<td>\
					<button class="btn btn-rounded btn-success" v-if="item.isInSign" v-on:click="$emit(\'begain\',item)">开始签到</button>\
					<button class="btn btn-rounded btn-warning" v-if="!item.isInSign" v-on:click="$emit(\'goon\',item)">继续签到</button>\
					<button class="btn btn-rounded btn-danger" v-if="!item.isInSign" v-on:click="$emit(\'stop\',item)">停止签到</button>\
			</td>\
		</tr>\
	',
	props: ['item']
});
Vue.component('class-notice-item',{
	props:['item'],
	template:'\
		<li class="nav-item"\
			\
			v-bind:key = "item.ClassId" \
			\
			v-on:click ="$emit(\'changeclass\',item)"\
			>\
			<a href="#"   class="nav-link">\
				<i class="icon icon-target"></i>{{item.className}}-{{item.classId}}\
			</a>\
		</li>\
	\
	',
});
Vue.component('item_作业',{
    data:function(){
        var that = this;
        that.t1=window.setInterval(refreshCount, 1000);
        function refreshCount() {
          that.date = new Date();
        }
        return{
            message:window.localStorage.getItem("name"),
            date:new Date(),
            t1:null
        }
    },
    template:'                    <div class="jumbotron">\
	<h1>{{message}}老师，作业功能&nbsp&nbsp&nbsp	即将开放，敬请期待！</h1> \
	<p>现在是，{{date}}</p> \
  </div>\
',	
});
Vue.component('item_小纸条',{
    data:function(){
        var that = this;
        that.t1=window.setInterval(refreshCount, 1000);
        function refreshCount() {
          that.date = new Date();
        }
        return{
            message:window.localStorage.getItem("name"),
            date:new Date(),
            t1:null
        }
    },
    template:'                    <div class="jumbotron">\
	<h1>{{message}}老师，小纸条功能&nbsp&nbsp&nbsp即将开放，敬请期待！</h1> \
	<p>现在是，{{date}}</p> \
  </div>\
',	
});
var app1 = new Vue({
    el:'#app1',
    data:{
        user:'',
		password:'',
    },
    methods: {
        login_tea:function(e){
            var that = this
            that.user = document.getElementById("user").value
            if(that.user == window.localStorage.getItem("user")){
                document.getElementById("password").value = localStorage.getItem("password")
            }
            that.password = document.getElementById("password").value
            axios.get('api/teacher',{
                params:{
                    TeacherId:that.user,
                    TeacherPasswd:that.password
                } 
            })
            .then(function (response) {
                console.log(response.data);
                window.localStorage.setItem("teacherId",response.data.teacherId);
                window.localStorage.setItem("name",response.data.name);
				window.localStorage.setItem("user",that.user);
				//if(window.sessionStorage.getItem("loginId"))
                	window.localStorage.setItem("password",that.password);
                window.location.href='index.html'; 
				window.localStorage.setItem("login",'1');
            })
            .catch(function (error) {
				console.log(error);
				alert("登录失败");
            });
        }
    },  
});

var app2 = new Vue({
    el:"#app2",
    data:{
		loginId:false,

    },
    methods:{
    }
});

var app3 = new Vue({
    el:"#app3",
    data:{
        currentTab:'首页',
        tabs:['首页','签到','作业','通知','小纸条'],
		tab:'',
        Items: [],
        TeacherId:window.localStorage.getItem("teacherId"),
		url:'api/SignIn/',
		nowCompent:null,
		hasReset:true
    },
    computed: {
        gb:function(){

            return 'item_' + this.currentTab;
        }
	},
	methods:{
		getData(){
			var vm =this;
			var url = vm.url+vm.TeacherId;
			axios.get(url)
			.then(function (response) {
					vm.Items = response.data;
			})
			.catch(function (error) {
				console.log(error);
			});	
		},
		reload(){
			var vm = this;
			vm.hasReset = true;
			console.log(vm.hasReset);
		}
		,
		changeclass(e){
			var vm = this;
			window.localStorage.setItem("ClassId",e.classId);

			vm.nowClassId=e.classId;

			console.log(window.localStorage.getItem("ClassId"));
			vm.hasReset = false;
			console.log(vm.hasReset);
			window.setTimeout(vm.reload,1000);
			

		}
	},
	created(){
		this.getData();
	}
});

var app4 = new Vue({
    el:"#app4",
    data:{
		user:''
    },
    method:{
	},
	created(){
		this.user=window.localStorage.getItem("name");
	}
});

function getPassword(){
	var that =this;
	console.log("开始匹配本地账号");
	that.user = document.getElementById("user").value
	if(that.user == window.localStorage.getItem("user")){
		console.log("得到已存在在本地的账号，现在对其进行本地密码匹配");
		document.getElementById("password").value = window.localStorage.getItem("password")
	}	
	else
	document.getElementById("password").value = ''
};

function getlogin(){
	var that = this;
	if(!that.loginId){
		that.loginId = true;
		window.sessionStorage.setItem("loginId",that.loginId);
	}
	else{
		that.loginId = false;
		window.sessionStorage.setItem("loginId",that.loginId);
	}
};


