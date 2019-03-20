var studentTable = new Vue({
	el:"#studentTable",
	data:{
		products:[],
		url:'api/signin',
		ClassId:'Cd11',
		SignInNums:1,
		ti:null,
		HasSignIn:0,
		UnHasSignIn:100,
		styleObjectHas:{
			width:0+"%"
		},
		styleObjectUnHas:{
			width:100+ "%"
		}
	},
	methods: {
		//向服务器请求当前点名次数的学生点名情况
		refreshStudent () {
			var vm = this;
			axios.get(vm.url,{
				params:{
					ClassId:vm.ClassId,
					SignInNums:vm.SignInNums
				}
			})
				.then(function (response) {
					vm.products = response.data;
					var unSignInCount = 0;
					response.data.forEach(element => {
						if(element.studentHasSignIn != true){
							element.studentSignInTime = "N/A"
							unSignInCount++;
						}
													
					});
					
					vm.UnHasSignIn = (unSignInCount/response.data.length)*100;
					vm.HasSignIn = 100 - vm.UnHasSignIn;
					vm.styleObjectHas.width = vm.HasSignIn + "%";
					vm.styleObjectUnHas.width = vm.UnHasSignIn + "%";
				})
				.catch(function (error) {
					vm.products = 'Error! Could not reach the API. ' + error
				})	
			}
	},
	created() {
		var vm = this;
		vm.refreshStudent();
	},
	mounted() {
		var vm = this;
		vm.ti = window.setInterval(vm.refreshStudent, 5000);

	},
	beforeDestroy() {
		clearInterval(this.ti);
	},
});

var QrCode = new Vue({
	el:"#QrCode",
	data:{
		url:'api/QrCode',
		classId:'Cd11',
		imgInfo:{
			src:'',
			codeText:''
		},
		ti:null,
	},
	methods:{
		getQrCode(){
			var vm = this;
			axios.get(vm.url,{
				params:{
					classId:vm.classId
				}
			})
			.then(function(response){
				vm.$set(vm.imgInfo,'src',response.data.qrCodePath +'?a=' + Math.random());
				vm.$set(vm.imgInfo,'codeText',response.data.qrCodeStr);
				vm.$forceUpdate();
			})	
		},
		stopSignIn(){
			var vm = this;
			window.clearInterval(vm.ti);
			
		},
		suspendSignIn(){
			var vm = this;
			window.clearInterval(vm.ti);
		},
		continueSignIn(){
			var vm = this;
			vm.getQrCode();
			vm.ti = window.setInterval(vm.getQrCode, 15000);
		},
	},
	created(){
		var vm = this;
		console.log("2222222");
		vm.ti = window.setInterval(vm.getQrCode, 15000);
		vm.getQrCode();
	}
})