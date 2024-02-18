<template>
  <div class="w-[260px] h-[100%] bg-[#fff] rounded-[10px] p-[12px] shadow-md">
    <!-- Logo -->
    <div class="flex h-[60px] mb-[12px]">
      <div class="flex items-center mr-[16px]">
        <img src="../../../assets/imgs/logo.png" class="h-[40px]" alt="" />
      </div>
      <h1 class="w-[100%] flex items-center">Minh Đăng</h1>
    </div>
    <div class="">
      <div class="">
        <router-link
          class="flex items-center h-[36px] rounded-[10px] px-[8px] hover:bg-[#eee]"
          to="/dashboard"
        >
          <div class="w-[36px] h-[36px] flex items-center">
            <DIcon
              iconName="mdi mdi-view-dashboard"
              iconColor="#32475c99"
            ></DIcon>
            <!-- <w-icon class="text-[#32475c99]">mdi mdi-view-dashboard</w-icon> -->
          </div>

          <div class="w-[calc(100% - 36px)] text-[#32475c99] align-middle">
            Dash Board
          </div>
        </router-link>
      </div>
      <div>
        <router-link
          class="flex items-center h-[36px] rounded-[10px] px-[8px] hover:bg-[#eee]"
          to="/garage"
        >
          <div class="w-[36px] h-[36px] flex items-center">
            <DIcon
              iconName="mdi mdi-garage-variant"
              iconColor="#32475c99"
            ></DIcon>
            <!-- <w-icon class="text-[#32475c99]">mdi mdi-view-dashboard</w-icon> -->
          </div>

          <div class="w-[calc(100% - 36px)] text-[#32475c99] align-middle">
            Bãi đỗ xe
          </div>
        </router-link>
      </div>
      <div>
        <router-link
          class="flex items-center h-[36px] rounded-[10px] px-[8px] hover:bg-[#eee]"
          to="/employee"
        >
          <div class="w-[36px] h-[36px] flex items-center">
            <DIcon iconName="mdi mdi-briefcase" iconColor="#32475c99"></DIcon>
            <!-- <w-icon class="text-[#32475c99]">mdi mdi-view-dashboard</w-icon> -->
          </div>

          <div class="w-[calc(100% - 36px)] text-[#32475c99] align-middle">
            Nhân viên
          </div>
        </router-link>
      </div>

      <div>
        <router-link
          class="flex items-center h-[36px] rounded-[10px] px-[8px] hover:bg-[#eee]"
          to="/parkmember"
        >
          <div class="w-[36px] h-[36px] flex items-center">
            <DIcon
              iconName="mdi mdi-clipboard-account-outline"
              iconColor="#32475c99"
            ></DIcon>
            <!-- <w-icon class="text-[#32475c99]">mdi mdi-view-dashboard</w-icon> -->
          </div>

          <div class="w-[calc(100% - 36px)] text-[#32475c99] align-middle">
            Khách hàng gửi xe
          </div>
        </router-link>
      </div>

      <div>
        <router-link
          class="flex items-center h-[36px] rounded-[10px] px-[8px] hover:bg-[#eee]"
          to="/parkinghistory-chart"
        >
          <div class="w-[36px] h-[36px] flex items-center">
            <DIcon
              iconName="mdi mdi-cash-multiple"
              iconColor="#32475c99"
            ></DIcon>
            <!-- <w-icon class="text-[#32475c99]">mdi mdi-view-dashboard</w-icon> -->
          </div>

          <div class="w-[calc(100% - 36px)] text-[#32475c99] align-middle">
            Doanh thu bãi đỗ xe
          </div>
        </router-link>
      </div>

      <div>
        <div>
          <div
            class="flex items-center h-[36px] rounded-[10px] px-[8px] mt-[12px] mb-[4px] hover:bg-[#eee]"
          >
            <div
              class="w-[calc(100% - 36px)] text-[#555] align-middle uppercase"
            >
              Tài khoản
            </div>
          </div>
        </div>
        <div>
          <div
            class="flex items-center h-[36px] rounded-[10px] px-[8px] cursor-pointer hover:bg-[#eee]"
            @click="userStore.signOut"
          >
            <div class="w-[36px] h-[36px] flex items-center">
              <DIcon
                iconName="mdi mdi-account-arrow-left"
                iconColor="#32475c99"
              ></DIcon>
              <!-- <w-icon class="text-[#32475c99]">mdi mdi-view-dashboard</w-icon> -->
            </div>

            <div class="w-[calc(100% - 36px)] text-[#32475c99] align-middle">
              Đăng xuất
            </div>
          </div>
        </div>
        <div>
          <div
            class="flex items-center h-[36px] rounded-[10px] px-[8px] cursor-pointer"
          >
            <div class="w-[50px] h-[36px] flex items-center">
              <w-switch class="ma2" :model-value="true" color="success">
              </w-switch>
            </div>

            <div class="w-[calc(100% - 50px)] text-[#32475c99] align-middle">
              Tiếng Việt
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { useUserStore } from "@/stores/UserStore";

const userStore = useUserStore();
</script>

<style lang="scss" scoped>
.router-link-active.router-link-exact-active i,
.router-link-active.router-link-exact-active div {
  color: #089740 !important;
}

.router-link-active.router-link-exact-active {
  background-color: #ceead9;
}
</style>
